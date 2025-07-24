namespace Repository.Interfaces;

public interface IDataTableRepository
{
    Task<DatatableResponse<T>> GetDataAsync<T>(IQueryable<T> query, DatatableRequest request) where T : class;
}
