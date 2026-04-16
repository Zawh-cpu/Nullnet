namespace Database.Application.SearchPolicies;

public record Pagination(UInt64 Limit, UInt64 Offset);

public class SearchPolicy
{
    public Pagination Pagination { get; set; } = new(50, 0);
}