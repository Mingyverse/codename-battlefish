using System.Collections.Generic;
using CodenameBattleFish.Fish;

namespace CodenameBattleFish.Habitat;

public class Habitat
{
    public HabitatType HabitatType;
    public List<SpawnPos<BattleFish>> FishPos;
    public List<SpawnPos<Decoration>> DecorationPos;
}