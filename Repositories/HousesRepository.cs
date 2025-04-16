



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

    string sql = @"

    SELECT 
    houses.*,
    accounts.* 
    FROM houses 
    INNER JOIN accounts ON accounts.id = houses.creator_id;";

    List<House> houses = _db.Query(sql, (House house, Account account) =>
    {

      house.Creator = account;
      return house;

    }).ToList();
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

  internal House CreateHouse(House houseData)
  {
    string sql = @"
INSERT INTO 
  houses (sqft, bedrooms, bathrooms, img_url, description, price, creator_id)
  VALUES (@Sqft, @Bedrooms, @Bathrooms, @ImgUrl, @Description, @Price, @CreatorId);


SELECT houses.*,
  accounts.* 
  FROM houses 
  INNER JOIN accounts on accounts.id = houses.creator_id
  WHERE houses.id = LAST_INSERT_ID();";

    House createdHouse = _db.Query(sql, (House house, Account account) =>
    {
      house.Creator = account;
      return house;
    }, houseData).SingleOrDefault();

    return createdHouse;
  }

  internal void DeleteHouse(int houseId)
  {
    string sql = "DELETE FROM houses WHERE id = @houseId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { houseId });

    if (rowsAffected == 0)
    {
      throw new Exception("No Rows Were Delete");
    }

    if (rowsAffected > 1)
    {
      throw new Exception(rowsAffected + "rows were affected. BAD NEWS!!!");
    }

  }

  internal void UpdateHouse(House house)
  {
string sql = @"

UPDATE houses
SET
sqft = @Sqft,
bathrooms = @Bathrooms,
bedrooms = @Bedrooms,
description = @Description,
img_url = @ImgUrl,
price = @Price
WHERE id = @Id
LIMIT 1;";

int rowsAffected = _db.Execute(sql, house);

  if (rowsAffected == 0)
    {
      throw new Exception("No Rows Were Updated");
    }

    if (rowsAffected > 1)
    {
      throw new Exception(rowsAffected + "rows were affected. BAD NEWS!!!");
    }
  }
}