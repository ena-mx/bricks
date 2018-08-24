# bricks
Common Application Components for fast aplication development.
## DataBricks (EnaBricks.DataBricks) SqlDeserializerComponent
Use for fast deserialize of DTO´s using XmlSerializer, either from Sql store procedure or a command text. The command should output only an xml column:
### Sql command
```sql
  SELECT CONVERT(XML, 
        SELECT
            CustomerId,
            Description
        FROM Cust_table
        For XML PATH('Customer'), ELEMENTS)
```
### C# Dto
```c#
public class Customer
    {
        public int CustomerId { get; set; }
        public string Description { get; set; }
    }
```
### Usage in program
```c#
public async Task DoWork()
        {
            using (SqlDeserializerComponent<Customer> dataComponent = 
                new SqlDeserializerComponent<Customer>(_connStringName, _cmdTxt, "Customers", new SqlParameter[]
                {
                    new SqlParameter("@companyId", Guid.NewGuid())
                }))
            {
                await dataComponent.ExecuteCmdTextAndDeserialize();
            }
        }
```
## DataBricks (EnaBricks.DataBricks) SqlCommandHelper
Helper wrapper over ExecuteNonQueryAsync and ExecuteScalarAsync command
### Program Sample
```c#
public async Task DoWork()
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper(_connStringName);
            bool isStoreProcudure = true;
            await commandHelper.ExecuteNonQueryAsync(_cmdTxt, isStoreProcudure);
            await commandHelper.ExecuteNonQueryAsync(_cmdTxt, isStoreProcudure, new SqlParameter[]
                {
                    new SqlParameter("@companyId", Guid.NewGuid())
                });

            int id1 = Convert.ToInt32(await commandHelper.ExecuteScalarAsync(_cmdTxt, isStoreProcudure));
            long id2 = Convert.ToInt64(await commandHelper.ExecuteScalarAsync(_cmdTxt, isStoreProcudure, new SqlParameter[]
                {
                    new SqlParameter("@companyId", Guid.NewGuid())
                }));
        }
```
