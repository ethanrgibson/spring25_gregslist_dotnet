

namespace gregslist_dotnet.Repositories;

public class HousesRepository
{

  public HousesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal List<House> GetAllHouses()
  {

    string sql = "SELECT * FROM houses;";

    List<House> houses = _db.Query<House>(sql).ToList();
    return houses;
  }

  internal House GetHouseById(int houseId)
  {

    string sql = @"

    SELECT houses.*,
      accounts.* 
      FROM houses 
      INNER JOIN accounts on accounts.id = houses.creator_id
      WHERE houses.id = @houseId;
    
    ";

    House house = _db.Query(sql, (House house, Account account) =>
    {

      house.Creator = account;
      return house;

    }, new { houseId }).SingleOrDefault();
    return house;


  }
}