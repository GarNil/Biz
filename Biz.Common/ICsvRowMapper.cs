namespace Biz.Common
{
    public interface ICsvRowMapper<TResult>
    {
        TResult Map((string[] v, int i) values);
    }
}
