using System.ComponentModel.DataAnnotations;

public record SetResponse
{
    public int SetId { get; init; } 
    public int Weight { get; init; }
    public int Reps { get; init; }
    public int Order { get; init; }

    public SetResponse(int id, int weight, int reps, int order)
        => (SetId, Weight, Reps, Order) = (id, weight, reps, order);
}