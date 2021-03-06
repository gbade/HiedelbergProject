## Problem
Potential for duplicate records due to race condition brough about by concurrency in insert operation.
This is a rare occurence in applications but one that still needs to be prevented as it can have devasting effects, depending on the criticality of the use case. 

## Cause
This duplicate can occur when a ScheduleItem entity is concurrently persisted to a database at the exact same time thereby eluding the more traditional duplicate checks -  check for existence before creating new record. For this operation, the validation done is via `ValidateDoesNotOverlapWithItems` which one checks for overlap in the schedule items. 

## Possible solutions 
There are different approaches to achieving concurrency control and their effectiveness depends on the exact operations (insert, update, delete, read) and the resulting problem. To prevent ScheduleItem duplication of records due to concurrency, these following as possible soltions;

1. Primary Key
Since primary keys also naturally come with UNIQUE KEY constraint, it can be effective in preventing duplicates. However, the ScheduleItem model's primary key - ScheduleItemId - is an database autogenerated (incremental) integer value, which is why it's unable to prevent duplicate since both records will have different Ids.

A way to potential use PK value still for this is to make more Id generation to the client. The could be done by making ScheduleItemId a guid (this elimates the burden of synchronised incremental value) and this is generated in the same transaction where the insert is done. This way, only one Schedule Item can be created successfully in a transaction. 

Hoever, this approach in itself isn't fail proof since duplication can still happen if the operation was done in different transaction or sessions. E.g in the very rare occasion when two different users manage to add the same entity at exactly the same time.

2. Transaction Isolation Level
A different and more globally effective approach is by using an appropriate isolation level. Since a transaction isolation level can be updated for a particular transaction. The insert operation can be done in a transaction with isolation level set to SERIALIZABLE. This will ensure that another transaction cannot insert new rows with `ScheduleItemId` values that would fall in the range of `ScheduleItemId` value as in the current transaction until the current transaction completes.

This approach, while effective, may be an overkill for this problem because of some of the lock consequences which include;
- More failed insert requests when the next key fall with range, and they will, since ScheduleItemId is incremental. The effect will be that no other insert (even when valid) will succeed while another one is in process in a different transaction.

- Implement good retry logic for such transient failures due to lock or nudge users to retry failed requests manually.
- Performance Implication and increased latency, especially as record volume grow.

3. Unique Key Constraint
This is a fairly straight forward approach as it involves updating the entity model schema to include unique constraint as a way to prevent duplication. The database will throw an exception when an operation will violate the set constraint. There are two ways to achieve this on `ScheduleItem`. 
- Introducing a new property/column called RecordHash with a unique key constraint. The value of this will be a hash of propeties `ScheduleId`, `start`, `end`, and `cement type`. E.g <ScheduleId>-<Start>-<End>-<CementType>
- Create a multiple column unique key constraint on the same columns - `ScheduleId`, `start`, `end`, and `cement type`

The downsides to approach 3a above is that a new column has to be created and there's also possibility of hash collision, very very rare. 

Generally, Unique constraint aproach may require some planning or database maintenance window because the process of updating db schema may take some time if it's an existing database with very volume amount of data. 


## Selected Solution Strategy
The preffered strategy for our use case and application complexity (low complexity) is Unique Key Constraint. Specifically, multicolumn unique key constraint as described in 3b above. Working on the assumption that they are no existing duplicates ScheduleItem in the db, this will be the least complicated change. 

Should there be duplicates in the system already, there has has to be a pre-process to remove such duplicates records prior to migrating the schema change.