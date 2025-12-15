public class Root
{
    public Charsheet Charsheet { get; set; } = null!;
}

public class Charsheet
{
    public string Preset { get; set; }
    public Profile Profile { get; set; }
    public Abilities Abilities { get; set; }
    public AbilitiesExtension AbilitiesExtension { get; set; }
    public Attributes Attributes { get; set; }
    public Backgrounds[] Backgrounds { get; set; }
    public Disciplines[] Disciplines { get; set; }
    public object[] DisciplinePaths { get; set; }
    public object[] Rituals { get; set; }
    public object[] Flaws { get; set; }
    public object[] Merits { get; set; }
    public string Notes { get; set; }
    public string CharHistory { get; set; }
    public string Goals { get; set; }
    public Virtues Virtues { get; set; }
    public State State { get; set; }
    public Health Health { get; set; }
    public HealthChimerical HealthChimerical { get; set; }
    public object[] Arts { get; set; }
    public Realms Realms { get; set; }
    public object[] OtherTraits { get; set; }
    public string AppearanceDescription { get; set; }
    public string CharacterImage { get; set; }
    public string AlliesAndContacts { get; set; }
    public string Possessions { get; set; }
    public object[] NuminaAndOtherTraits { get; set; }
    public Spheres Spheres { get; set; }
    public int Money { get; set; }
}

public class Profile
{
    public string Name { get; set; }
    public string Player { get; set; }
    public string Chronicle { get; set; }
    public string Nature { get; set; }
    public string Age { get; set; }
    public string Sex { get; set; }
    public string Demeanor { get; set; }
    public string Concept { get; set; }
    public string Clan { get; set; }
    public string Generation { get; set; }
    public string Sire { get; set; }
    public string Court { get; set; }
    public string House { get; set; }
    public string Kith { get; set; }
    public string PrimaryLegacy { get; set; }
    public string SecondaryLegacy { get; set; }
    public string Motley { get; set; }
    public string Seeming { get; set; }
    public string Residence { get; set; }
    public string Essence { get; set; }
    public string Affiliation { get; set; }
    public string Sect { get; set; }
}

public class Abilities
{
    public int Alertness { get; set; }
    public int Athletics { get; set; }
    public int Brawl { get; set; }
    public int Empathy { get; set; }
    public int Expression { get; set; }
    public int Intimidation { get; set; }
    public int Leadership { get; set; }
    public int Streetwise { get; set; }
    public int Subterfuge { get; set; }
    public int Awareness { get; set; }
    public int Animalken { get; set; }
    public int Crafts { get; set; }
    public int Drive { get; set; }
    public int Etiquette { get; set; }
    public int Firearms { get; set; }
    public int Melee { get; set; }
    public int Performance { get; set; }
    public int Stealth { get; set; }
    public int Survival { get; set; }
    public int Larceny { get; set; }
    public int Academics { get; set; }
    public int Computer { get; set; }
    public int Finance { get; set; }
    public int Investigation { get; set; }
    public int Law { get; set; }
    public int Medicine { get; set; }
    public int Occult { get; set; }
    public int Politics { get; set; }
    public int Science { get; set; }
    public int Technology { get; set; }
    public int Enigmas { get; set; }
    public int Gremayre { get; set; }
    public int Kenning { get; set; }
    public int Legerdemain { get; set; }
    public int Archery { get; set; }
    public int Commerce { get; set; }
    public int Ride { get; set; }
    public int HearthWisdom { get; set; }
    public int Seneschal { get; set; }
    public int Theology { get; set; }
    public int Dodge { get; set; }
    public int Security { get; set; }
    public int Linguistics { get; set; }
    public int Art { get; set; }
    public int MartialArts { get; set; }
    public int Meditation { get; set; }
    public int Research { get; set; }
    public int Cosmology { get; set; }
    public int EnigmasMta { get; set; }
    public int Esoterica { get; set; }
}

public class AbilitiesExtension
{
    public string TalentName1 { get; set; }
    public int TalentValue1 { get; set; }
    public string TalentName2 { get; set; }
    public int TalentValue2 { get; set; }
    public string SkillName1 { get; set; }
    public int SkillValue1 { get; set; }
    public string SkillName2 { get; set; }
    public int SkillValue2 { get; set; }
    public string KnowledgeName1 { get; set; }
    public int KnowledgeValue1 { get; set; }
    public string KnowledgeName2 { get; set; }
    public int KnowledgeValue2 { get; set; }
}

public class Attributes
{
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Stamina { get; set; }
    public int Charisma { get; set; }
    public int Manipulation { get; set; }
    public int Appearance { get; set; }
    public int Perception { get; set; }
    public int Intelligence { get; set; }
    public int Wits { get; set; }
}

public class Backgrounds
{
    public string Name { get; set; }
    public int Value { get; set; }
}

public class Disciplines
{
    public string Name { get; set; }
    public int Value { get; set; }
}

public class Virtues
{
    public int Conscience { get; set; }
    public int SelfControl { get; set; }
    public int Courage { get; set; }
}

public class State
{
    public int WillpowerRating { get; set; }
    public int WillpowerPool { get; set; }
    public int Experience { get; set; }
    public int Humanity { get; set; }
    public string PathName { get; set; }
    public string BearingName { get; set; }
    public string BearingModifier { get; set; }
    public int Bloodpool { get; set; }
    public string BloodPerTurn { get; set; }
    public string Weakness { get; set; }
    public string Antithesis { get; set; }
    public string Thresholds { get; set; }
    public string BirthrightsFrailties { get; set; }
    public int GlamourRating { get; set; }
    public int GlamourPool { get; set; }
    public int BanalityRating { get; set; }
    public int BanalityPool { get; set; }
    public int Nightmare { get; set; }
    public int Faith { get; set; }
    public int RoadValue { get; set; }
    public string RoadName { get; set; }
    public string AuraName { get; set; }
    public string AuraModifier { get; set; }
    public int Arete { get; set; }
    public int Quintessence { get; set; }
    public int Paradox { get; set; }
}

public class Health
{
    public int Bruised { get; set; }
    public int Hurt { get; set; }
    public int Injured { get; set; }
    public int Wounded { get; set; }
    public int Mauled { get; set; }
    public int Crippled { get; set; }
    public int Incapacitated { get; set; }
}

public class HealthChimerical
{
    public int Bruised { get; set; }
    public int Hurt { get; set; }
    public int Injured { get; set; }
    public int Wounded { get; set; }
    public int Mauled { get; set; }
    public int Crippled { get; set; }
    public int Incapacitated { get; set; }
}

public class Realms
{
    public int Actor { get; set; }
    public int Fae { get; set; }
    public int Nature { get; set; }
    public int Prop { get; set; }
    public int Scene { get; set; }
    public int Time { get; set; }
}

public class Spheres
{
    public int Correspondence { get; set; }
    public int Entropy { get; set; }
    public int Forces { get; set; }
    public int Life { get; set; }
    public int Matter { get; set; }
    public int Mind { get; set; }
    public int Prime { get; set; }
    public int Spirit { get; set; }
    public int Time { get; set; }
}

