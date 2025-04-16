

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

    string sql = "SELECT * FROM houses WHERE id = @houseId;";

    House house = _db.Query<House>(sql, new { houseId }).SingleOrDefault();
    return house;


  }
}