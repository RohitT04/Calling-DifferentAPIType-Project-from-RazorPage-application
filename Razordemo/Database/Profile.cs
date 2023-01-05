namespace Razordemo.Database;


public class Profile
{
    public long ID { get; set; }

    public string Customer_Name { get; set; }

    public string Mobile { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime InsertedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public long? InsertedId { get; set; }

    public long? ModifiedId { get; set; }

    public bool Active { get; set; }
    public string Status { get; set; }
}


