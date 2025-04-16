



namespace gregslist_dotnet.Services;

public class HousesService
{

  public HousesService(HousesRepository housesRepository)
  {
    _housesRepository = housesRepository;

  }
  private readonly HousesRepository _housesRepository;




  internal List<House> GetAllHouses()
  {
    List<House> houses = _housesRepository.GetAllHouses();
    return houses;

  }

  internal House GetHouseById(int houseId)
  {
    House house = _housesRepository.GetHouseById(houseId);
    return house;
  }

  internal House CreateHouse(House houseData)
  {
    House house = _housesRepository.CreateHouse(houseData);
    return house;
  }

  internal string DeleteHouse(int houseId, Account userInfo)
  {
    House house = GetHouseById(houseId);
    if (house.CreatorId != userInfo.Id)
    {
      throw new Exception("YOU CANNOT DELETE ANOTHER USERS HOUSES");
    }

    _housesRepository.DeleteHouse(houseId);

    return $"House {houseId}, was deleted. Forever and ever.";

  }

  internal House UpdateHouse(int houseId, House updatedHouseData, Account userInfo)
  {
    House house = GetHouseById(houseId);

    if (house.CreatorId != userInfo.Id)
    {
      throw new Exception("CANNOT UPDATE A HOUSE YOU DID NOT CREATE");
    }

    house.Sqft = updatedHouseData.Sqft ?? house.Sqft;
    house.Bathrooms = updatedHouseData.Bathrooms ?? house.Bathrooms;
    house.Bedrooms = updatedHouseData.Bedrooms ?? house.Bedrooms;
    house.Description = updatedHouseData.Description ?? house.Description;
    house.ImgUrl = updatedHouseData.ImgUrl ?? house.ImgUrl;
    house.Price = updatedHouseData.Price ?? house.Price;

    _housesRepository.UpdateHouse(house);

    return house;

  }
}