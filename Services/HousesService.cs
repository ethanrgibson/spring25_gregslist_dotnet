



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
}