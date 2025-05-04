using System.Data;

namespace AssociationsClean.Application.Shared.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
