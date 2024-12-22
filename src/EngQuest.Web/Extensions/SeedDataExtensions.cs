using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using EngQuest.Application.Abstractions.Data;
using EngQuest.Domain.Objectives;
using EngQuest.Domain.Shared;
using EngQuest.Domain.Vocabulary.Adverbs;
using EngQuest.Domain.Vocabulary.ComparisonAdjectives;
using EngQuest.Domain.Vocabulary.Compounds;
using EngQuest.Domain.Vocabulary.Nouns;
using EngQuest.Domain.Vocabulary.Pronouns;
using EngQuest.Domain.Vocabulary.Verbs;

// ReSharper disable UnusedMember.Local

namespace EngQuest.Web.Extensions;

[SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out")]
[SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed")]
[SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
internal static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ISqlConnectionFactory sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        InsertQuests(connection);
        // InsertObjectives(connection);
        // InsertObjectiveQuestIds(connection);
        // InsertWords(connection);
        
        InsertAdjectives(connection);
        InsertAdverbs(connection);
        InsertComparisonAdjectives(connection);
        InsertCompounds(connection);
        InsertDeterminers(connection);
        InsertLanguages(connection);
        InsertLetterNumbers(connection);
        InsertModalVerbs(connection);
        InsertNouns(connection);
        InsertPrepositions(connection);
        InsertPrimaryVerbs(connection);
        InsertAdditionalForms(connection);
        InsertShortNegativeForms(connection);
        InsertFullNegativeForms(connection);
        InsertPronouns(connection);
        InsertQuestionWords(connection);
        InsertVerbs(connection);
        InsertCities(connection);
    }

    private static void InsertVerbs(IDbConnection connection)
    {
        object[] verbs = GetVerbs();

        const string verbsSql = """
                                INSERT INTO public.verbs (text, past_form, past_participle_form, present_participle_form, third_person_form, is_irregular_verb)
                                VALUES (@Text, @PastForm, @PastParticipleForm, @PresentParticipleForm, @ThirdPersonForm, @IsIrregularVerb);
                                """;

        connection.Execute(verbsSql, verbs);
    }

    private static void InsertCities(IDbConnection connection)
    {
        object[] cities = GetCities();

        const string citiesSql = """
                                INSERT INTO public.cities (text)
                                VALUES (@Text);
                                """;

        connection.Execute(citiesSql, cities);
    }

    private static object[] GetCities()
    {
        object[] cities =
        [
            new { Text = "hong kong" },
            new { Text = "bangkok" },
            new { Text = "london" },
            new { Text = "macau" },
            new { Text = "singapore" },
            new { Text = "paris" },
            new { Text = "dubai" },
            new { Text = "new york city" },
            new { Text = "kuala lumpur" },
            new { Text = "istanbul" },
            new { Text = "delhi" },
            new { Text = "antalya" },
            new { Text = "shenzhen" },
            new { Text = "mumbai" },
            new { Text = "phuket" },
            new { Text = "rome" },
            new { Text = "tokyo" },
            new { Text = "pattaya" },
            new { Text = "taipei" },
            new { Text = "mecca" },
            new { Text = "guangzhou" },
            new { Text = "prague" },
            new { Text = "medina" },
            new { Text = "seoul" },
            new { Text = "amsterdam" },
            new { Text = "agra" },
            new { Text = "miami" },
            new { Text = "osaka" },
            new { Text = "las vegas" },
            new { Text = "shanghai" },
            new { Text = "ho chi minh city" },
            new { Text = "denpasar" },
            new { Text = "barcelona" },
            new { Text = "los angeles" },
            new { Text = "milan" },
            new { Text = "chennai" },
            new { Text = "vienna" },
            new { Text = "johor bahru" },
            new { Text = "jaipur" },
            new { Text = "cancun" },
            new { Text = "berlin" },
            new { Text = "cairo" },
            new { Text = "athens" },
            new { Text = "orlando" },
            new { Text = "moscow" },
            new { Text = "venice" },
            new { Text = "madrid" },
            new { Text = "ha long" },
            new { Text = "riyadh" },
            new { Text = "dublin" },
            new { Text = "florence" },
            new { Text = "hanoi" },
            new { Text = "toronto" },
            new { Text = "johannesburg" },
            new { Text = "sydney" },
            new { Text = "munich" },
            new { Text = "jakarta" },
            new { Text = "beijing" },
            new { Text = "saint petersburg" },
            new { Text = "brussels" },
            new { Text = "budapest" },
            new { Text = "jerusalem" },
            new { Text = "lisbon" },
            new { Text = "dammam" },
            new { Text = "penang island" },
            new { Text = "heraklion" },
            new { Text = "kyoto" },
            new { Text = "zhuhai" },
            new { Text = "vancouver" },
            new { Text = "chiang mai" },
            new { Text = "copenhagen" },
            new { Text = "san francisco" },
            new { Text = "melbourne" },
            new { Text = "warsaw" },
            new { Text = "marrakesh" },
            new { Text = "kolkata" },
            new { Text = "cebu city" },
            new { Text = "auckland" },
            new { Text = "tel aviv" },
            new { Text = "guilin" },
            new { Text = "honolulu" },
            new { Text = "hurghada" },
            new { Text = "krakow" },
            new { Text = "mugla" },
            new { Text = "buenos aires" },
            new { Text = "chiba" },
            new { Text = "frankfurt am main" },
            new { Text = "stockholm" },
            new { Text = "lima" },
            new { Text = "da nang" },
            new { Text = "batam" },
            new { Text = "fukuoka" },
            new { Text = "abu dhabi" },
            new { Text = "jeju" },
            new { Text = "porto" },
            new { Text = "rhodes" },
            new { Text = "rio de janeiro" },
            new { Text = "krabi" },
            new { Text = "bangalore" },
            new { Text = "mexico city" },
            new { Text = "punta cana" },
            new { Text = "são paulo" },
            new { Text = "zurich" },
            new { Text = "montreal" },
            new { Text = "washington" },
            new { Text = "chicago" },
            new { Text = "dusseldorf" },
            new { Text = "boston" },
            new { Text = "chengdu" },
            new { Text = "edinburgh" },
            new { Text = "san jose" },
            new { Text = "tehran" },
            new { Text = "houston" },
            new { Text = "hamburg" },
            new { Text = "cape town" },
            new { Text = "manila" },
            new { Text = "bogota" },
            new { Text = "beirut" },
            new { Text = "geneva" },
            new { Text = "colombo" },
            new { Text = "xiamen" },
            new { Text = "bucharest" },
            new { Text = "casablanca" },
            new { Text = "atlanta" },
            new { Text = "sofia" },
            new { Text = "dalian" },
            new { Text = "montevideo" },
            new { Text = "amman" },
            new { Text = "hangzhou" },
            new { Text = "pune" },
            new { Text = "durban" },
            new { Text = "dallas" },
            new { Text = "accra" },
            new { Text = "quito" },
            new { Text = "tianjin" },
            new { Text = "qingdao" },
            new { Text = "philadelphia" },
            new { Text = "lagos" },

        ];

        return cities;
    }

    private static object[] GetVerbs()
    {
        Verb[] generatedVerbs =
        [
            Verb.CreateIrregularVerb(new Text("abide"), new PastForm("abode"), new PastParticipleForm("abode")),
            Verb.CreateIrregularVerb(new Text("arise"), new PastForm("arose"), new PastParticipleForm("arisen")),
            Verb.CreateIrregularVerb(new Text("awake"), new PastForm("awoke"), new PastParticipleForm("awoken")),
            Verb.CreateIrregularVerb(new Text("bear"), new PastForm("bore"), new PastParticipleForm("born")),
            Verb.CreateIrregularVerb(new Text("beat"), new PastForm("beat"), new PastParticipleForm("beaten")),
            Verb.CreateIrregularVerb(new Text("become"), new PastForm("became"), new PastParticipleForm("become")),
            Verb.CreateIrregularVerb(new Text("beget"), new PastForm("begot"), new PastParticipleForm("begotten")),
            Verb.CreateIrregularVerb(new Text("begin"), new PastForm("began"), new PastParticipleForm("begun")),
            Verb.CreateIrregularVerb(new Text("bend"), new PastForm("bent"), new PastParticipleForm("bent")),
            Verb.CreateIrregularVerb(new Text("bet"), new PastForm("bet"), new PastParticipleForm("bet")),
            Verb.CreateIrregularVerb(new Text("bid"), new PastForm("bade"), new PastParticipleForm("bidden")),
            Verb.CreateIrregularVerb(new Text("bite"), new PastForm("bit"), new PastParticipleForm("bitten")),
            Verb.CreateIrregularVerb(new Text("bleed"), new PastForm("bled"), new PastParticipleForm("bled")),
            Verb.CreateIrregularVerb(new Text("blow"), new PastForm("blew"), new PastParticipleForm("blown")),
            Verb.CreateIrregularVerb(new Text("break"), new PastForm("broke"), new PastParticipleForm("broken")),
            Verb.CreateIrregularVerb(new Text("bring"), new PastForm("brought"), new PastParticipleForm("brought")),
            Verb.CreateIrregularVerb(new Text("broadcast"), new PastForm("broadcast"), new PastParticipleForm("broadcast")),
            Verb.CreateIrregularVerb(new Text("build"), new PastForm("built"), new PastParticipleForm("built")),
            Verb.CreateIrregularVerb(new Text("burn"), new PastForm("burnt"), new PastParticipleForm("burnt")),
            Verb.CreateIrregularVerb(new Text("burst"), new PastForm("burst"), new PastParticipleForm("burst")),
            Verb.CreateIrregularVerb(new Text("buy"), new PastForm("bought"), new PastParticipleForm("bought")),
            Verb.CreateIrregularVerb(new Text("cast"), new PastForm("cast"), new PastParticipleForm("cast")),
            Verb.CreateIrregularVerb(new Text("catch"), new PastForm("caught"), new PastParticipleForm("caught")),
            Verb.CreateIrregularVerb(new Text("chide"), new PastForm("chid"), new PastParticipleForm("chidden")),
            Verb.CreateIrregularVerb(new Text("choose"), new PastForm("chose"), new PastParticipleForm("chosen")),
            Verb.CreateIrregularVerb(new Text("cling"), new PastForm("clung"), new PastParticipleForm("clung")),
            Verb.CreateIrregularVerb(new Text("clothe"), new PastForm("clad"), new PastParticipleForm("clad")),
            Verb.CreateIrregularVerb(new Text("come"), new PastForm("came"), new PastParticipleForm("come")),
            Verb.CreateIrregularVerb(new Text("cost"), new PastForm("cost"), new PastParticipleForm("cost")),
            Verb.CreateIrregularVerb(new Text("creep"), new PastForm("crept"), new PastParticipleForm("crept")),
            Verb.CreateIrregularVerb(new Text("cut"), new PastForm("cut"), new PastParticipleForm("cut")),
            Verb.CreateIrregularVerb(new Text("deal"), new PastForm("dealt"), new PastParticipleForm("dealt")),
            Verb.CreateIrregularVerb(new Text("dig"), new PastForm("dug"), new PastParticipleForm("dug")),
            Verb.CreateIrregularVerb(new Text("dive"), new PastForm("dived"), new PastParticipleForm("dove")),
            Verb.CreateIrregularVerb(new Text("draw"), new PastForm("drew"), new PastParticipleForm("drawn")),
            Verb.CreateIrregularVerb(new Text("dream"), new PastForm("dreamt"), new PastParticipleForm("dreamt")),
            Verb.CreateIrregularVerb(new Text("drink"), new PastForm("drank"), new PastParticipleForm("drunk")),
            Verb.CreateIrregularVerb(new Text("drive"), new PastForm("drove"), new PastParticipleForm("driven")),
            Verb.CreateIrregularVerb(new Text("dwell"), new PastForm("dwelt"), new PastParticipleForm("dwelt")),
            Verb.CreateIrregularVerb(new Text("eat"), new PastForm("ate"), new PastParticipleForm("eaten")),
            Verb.CreateIrregularVerb(new Text("fall"), new PastForm("fell"), new PastParticipleForm("fallen")),
            Verb.CreateIrregularVerb(new Text("feed"), new PastForm("fed"), new PastParticipleForm("fed")),
            Verb.CreateIrregularVerb(new Text("feel"), new PastForm("felt"), new PastParticipleForm("felt")),
            Verb.CreateIrregularVerb(new Text("fight"), new PastForm("fought"), new PastParticipleForm("fought")),
            Verb.CreateIrregularVerb(new Text("find"), new PastForm("found"), new PastParticipleForm("found")),
            Verb.CreateIrregularVerb(new Text("flee"), new PastForm("fled"), new PastParticipleForm("fled")),
            Verb.CreateIrregularVerb(new Text("fling"), new PastForm("flung"), new PastParticipleForm("flung")),
            Verb.CreateIrregularVerb(new Text("fly"), new PastForm("flew"), new PastParticipleForm("flown")),
            Verb.CreateIrregularVerb(new Text("forbid"), new PastForm("forbade"), new PastParticipleForm("forbidden")),
            Verb.CreateIrregularVerb(new Text("forecast"), new PastForm("forecast"), new PastParticipleForm("forecast")),
            Verb.CreateIrregularVerb(new Text("foresee"), new PastForm("foresaw"), new PastParticipleForm("foreseen")),
            Verb.CreateIrregularVerb(new Text("forget"), new PastForm("forgot"), new PastParticipleForm("forgotten")),
            Verb.CreateIrregularVerb(new Text("forgive"), new PastForm("forgave"), new PastParticipleForm("forgiven")),
            Verb.CreateIrregularVerb(new Text("forsake"), new PastForm("forsook"), new PastParticipleForm("forsaken")),
            Verb.CreateIrregularVerb(new Text("freeze"), new PastForm("froze"), new PastParticipleForm("frozen")),
            Verb.CreateIrregularVerb(new Text("get"), new PastForm("got"), new PastParticipleForm("gotten")),
            Verb.CreateIrregularVerb(new Text("give"), new PastForm("gave"), new PastParticipleForm("given")),
            Verb.CreateIrregularVerb(new Text("go"), new PastForm("went"), new PastParticipleForm("gone")),
            Verb.CreateIrregularVerb(new Text("grind"), new PastForm("ground"), new PastParticipleForm("ground")),
            Verb.CreateIrregularVerb(new Text("grow"), new PastForm("grew"), new PastParticipleForm("grown")),
            Verb.CreateIrregularVerb(new Text("hang"), new PastForm("hung"), new PastParticipleForm("hung")),
            Verb.CreateIrregularVerb(new Text("hear"), new PastForm("heard"), new PastParticipleForm("heard")),
            Verb.CreateIrregularVerb(new Text("hide"), new PastForm("hid"), new PastParticipleForm("hidden")),
            Verb.CreateIrregularVerb(new Text("hit"), new PastForm("hit"), new PastParticipleForm("hit")),
            Verb.CreateIrregularVerb(new Text("hold"), new PastForm("held"), new PastParticipleForm("held")),
            Verb.CreateIrregularVerb(new Text("hurt"), new PastForm("hurt"), new PastParticipleForm("hurt")),
            Verb.CreateIrregularVerb(new Text("keep"), new PastForm("kept"), new PastParticipleForm("kept")),
            Verb.CreateIrregularVerb(new Text("kneel"), new PastForm("knelt"), new PastParticipleForm("knelt")),
            Verb.CreateIrregularVerb(new Text("know"), new PastForm("knew"), new PastParticipleForm("known")),
            Verb.CreateIrregularVerb(new Text("lay"), new PastForm("laid"), new PastParticipleForm("laid")),
            Verb.CreateIrregularVerb(new Text("lead"), new PastForm("led"), new PastParticipleForm("led")),
            Verb.CreateIrregularVerb(new Text("lean"), new PastForm("leant"), new PastParticipleForm("leant")),
            Verb.CreateIrregularVerb(new Text("leap"), new PastForm("leapt"), new PastParticipleForm("leapt")),
            Verb.CreateIrregularVerb(new Text("learn"), new PastForm("learnt"), new PastParticipleForm("learnt")),
            Verb.CreateIrregularVerb(new Text("leave"), new PastForm("left"), new PastParticipleForm("left")),
            Verb.CreateIrregularVerb(new Text("lend"), new PastForm("lent"), new PastParticipleForm("lent")),
            Verb.CreateIrregularVerb(new Text("let"), new PastForm("let"), new PastParticipleForm("let")),
            Verb.CreateIrregularVerb(new Text("lie"), new PastForm("lay"), new PastParticipleForm("lain")),
            Verb.CreateIrregularVerb(new Text("lose"), new PastForm("lost"), new PastParticipleForm("lost")),
            Verb.CreateIrregularVerb(new Text("make"), new PastForm("made"), new PastParticipleForm("made")),
            Verb.CreateIrregularVerb(new Text("mean"), new PastForm("meant"), new PastParticipleForm("meant")),
            Verb.CreateIrregularVerb(new Text("meet"), new PastForm("met"), new PastParticipleForm("met")),
            Verb.CreateIrregularVerb(new Text("mow"), new PastForm("mowed"), new PastParticipleForm("mown")),
            Verb.CreateIrregularVerb(new Text("offset"), new PastForm("offset"), new PastParticipleForm("offset")),
            Verb.CreateIrregularVerb(new Text("overcome"), new PastForm("overcame"), new PastParticipleForm("overcome")),
            Verb.CreateIrregularVerb(new Text("partake"), new PastForm("partook"), new PastParticipleForm("partaken")),
            Verb.CreateIrregularVerb(new Text("pay"), new PastForm("paid"), new PastParticipleForm("paid")),
            Verb.CreateIrregularVerb(new Text("plead"), new PastForm("pled"), new PastParticipleForm("pled")),
            Verb.CreateIrregularVerb(new Text("preset"), new PastForm("preset"), new PastParticipleForm("preset")),
            Verb.CreateIrregularVerb(new Text("prove"), new PastForm("proved"), new PastParticipleForm("proven")),
            Verb.CreateIrregularVerb(new Text("put"), new PastForm("put"), new PastParticipleForm("put")),
            Verb.CreateIrregularVerb(new Text("quit"), new PastForm("quit"), new PastParticipleForm("quit")),
            Verb.CreateIrregularVerb(new Text("read"), new PastForm("read"), new PastParticipleForm("read")),
            Verb.CreateIrregularVerb(new Text("relay"), new PastForm("relaid"), new PastParticipleForm("relaid")),
            Verb.CreateIrregularVerb(new Text("rend"), new PastForm("rent"), new PastParticipleForm("rent")),
            Verb.CreateIrregularVerb(new Text("rid"), new PastForm("rid"), new PastParticipleForm("rid")),
            Verb.CreateIrregularVerb(new Text("ring"), new PastForm("rang"), new PastParticipleForm("rung")),
            Verb.CreateIrregularVerb(new Text("rise"), new PastForm("rose"), new PastParticipleForm("risen")),
            Verb.CreateIrregularVerb(new Text("run"), new PastForm("ran"), new PastParticipleForm("run")),
            Verb.CreateIrregularVerb(new Text("say"), new PastForm("said"), new PastParticipleForm("said")),
            Verb.CreateIrregularVerb(new Text("see"), new PastForm("saw"), new PastParticipleForm("seen")),
            Verb.CreateIrregularVerb(new Text("seek"), new PastForm("sought"), new PastParticipleForm("sought")),
            Verb.CreateIrregularVerb(new Text("sell"), new PastForm("sold"), new PastParticipleForm("sold")),
            Verb.CreateIrregularVerb(new Text("send"), new PastForm("sent"), new PastParticipleForm("sent")),
            Verb.CreateIrregularVerb(new Text("set"), new PastForm("set"), new PastParticipleForm("set")),
            Verb.CreateIrregularVerb(new Text("shake"), new PastForm("shook"), new PastParticipleForm("shaken")),
            Verb.CreateIrregularVerb(new Text("shed"), new PastForm("shed"), new PastParticipleForm("shed")),
            Verb.CreateIrregularVerb(new Text("shine"), new PastForm("shone"), new PastParticipleForm("shone")),
            Verb.CreateIrregularVerb(new Text("shoe"), new PastForm("shod"), new PastParticipleForm("shod")),
            Verb.CreateIrregularVerb(new Text("shoot"), new PastForm("shot"), new PastParticipleForm("shot")),
            Verb.CreateIrregularVerb(new Text("show"), new PastForm("showed"), new PastParticipleForm("shown")),
            Verb.CreateIrregularVerb(new Text("shut"), new PastForm("shut"), new PastParticipleForm("shut")),
            Verb.CreateIrregularVerb(new Text("sing"), new PastForm("sang"), new PastParticipleForm("sung")),
            Verb.CreateIrregularVerb(new Text("sink"), new PastForm("sank"), new PastParticipleForm("sunk")),
            Verb.CreateIrregularVerb(new Text("sit"), new PastForm("sat"), new PastParticipleForm("sat")),
            Verb.CreateIrregularVerb(new Text("slay"), new PastForm("slew"), new PastParticipleForm("slain")),
            Verb.CreateIrregularVerb(new Text("sleep"), new PastForm("slept"), new PastParticipleForm("slept")),
            Verb.CreateIrregularVerb(new Text("slide"), new PastForm("slid"), new PastParticipleForm("slid")),
            Verb.CreateIrregularVerb(new Text("slit"), new PastForm("slit"), new PastParticipleForm("slit")),
            Verb.CreateIrregularVerb(new Text("smell"), new PastForm("smelt"), new PastParticipleForm("smelt")),
            Verb.CreateIrregularVerb(new Text("sow"), new PastForm("sowed"), new PastParticipleForm("sown")),
            Verb.CreateIrregularVerb(new Text("speak"), new PastForm("spoke"), new PastParticipleForm("spoken")),
            Verb.CreateIrregularVerb(new Text("speed"), new PastForm("sped"), new PastParticipleForm("sped")),
            Verb.CreateIrregularVerb(new Text("spell"), new PastForm("spelt"), new PastParticipleForm("spelt")),
            Verb.CreateIrregularVerb(new Text("spend"), new PastForm("spent"), new PastParticipleForm("spent")),
            Verb.CreateIrregularVerb(new Text("spill"), new PastForm("spilt"), new PastParticipleForm("spilt")),
            Verb.CreateIrregularVerb(new Text("spin"), new PastForm("spun"), new PastParticipleForm("spun")),
            Verb.CreateIrregularVerb(new Text("spit"), new PastForm("spat"), new PastParticipleForm("spat")),
            Verb.CreateIrregularVerb(new Text("split"), new PastForm("split"), new PastParticipleForm("split")),
            Verb.CreateIrregularVerb(new Text("spoil"), new PastForm("spoilt"), new PastParticipleForm("spoilt")),
            Verb.CreateIrregularVerb(new Text("spread"), new PastForm("spread"), new PastParticipleForm("spread")),
            Verb.CreateIrregularVerb(new Text("stand"), new PastForm("stood"), new PastParticipleForm("stood")),
            Verb.CreateIrregularVerb(new Text("steal"), new PastForm("stole"), new PastParticipleForm("stolen")),
            Verb.CreateIrregularVerb(new Text("stick"), new PastForm("stuck"), new PastParticipleForm("stuck")),
            Verb.CreateIrregularVerb(new Text("sting"), new PastForm("stung"), new PastParticipleForm("stung")),
            Verb.CreateIrregularVerb(new Text("stink"), new PastForm("stank"), new PastParticipleForm("stunk")),
            Verb.CreateIrregularVerb(new Text("strew"), new PastForm("strewed"), new PastParticipleForm("strewn")),
            Verb.CreateIrregularVerb(new Text("strike"), new PastForm("struck"), new PastParticipleForm("stricken")),
            Verb.CreateIrregularVerb(new Text("strive"), new PastForm("strove"), new PastParticipleForm("striven")),
            Verb.CreateIrregularVerb(new Text("swear"), new PastForm("swore"), new PastParticipleForm("sworn")),
            Verb.CreateIrregularVerb(new Text("sweat"), new PastForm("sweat"), new PastParticipleForm("sweat")),
            Verb.CreateIrregularVerb(new Text("sweep"), new PastForm("swept"), new PastParticipleForm("swept")),
            Verb.CreateIrregularVerb(new Text("swell"), new PastForm("sweated"), new PastParticipleForm("swollen")),
            Verb.CreateIrregularVerb(new Text("swim"), new PastForm("swam"), new PastParticipleForm("swum")),
            Verb.CreateIrregularVerb(new Text("swing"), new PastForm("swung"), new PastParticipleForm("swung")),
            Verb.CreateIrregularVerb(new Text("take"), new PastForm("took"), new PastParticipleForm("taken")),
            Verb.CreateIrregularVerb(new Text("teach"), new PastForm("taught"), new PastParticipleForm("taught")),
            Verb.CreateIrregularVerb(new Text("tear"), new PastForm("tore"), new PastParticipleForm("torn")),
            Verb.CreateIrregularVerb(new Text("tell"), new PastForm("told"), new PastParticipleForm("told")),
            Verb.CreateIrregularVerb(new Text("think"), new PastForm("thought"), new PastParticipleForm("thought")),
            Verb.CreateIrregularVerb(new Text("thrive"), new PastForm("throve"), new PastParticipleForm("thriven")),
            Verb.CreateIrregularVerb(new Text("throw"), new PastForm("threw"), new PastParticipleForm("thrown")),
            Verb.CreateIrregularVerb(new Text("thrust"), new PastForm("thrust"), new PastParticipleForm("thrust")),
            Verb.CreateIrregularVerb(new Text("typeset"), new PastForm("typeset"), new PastParticipleForm("typeset")),
            Verb.CreateIrregularVerb(new Text("undergo"), new PastForm("underwent"), new PastParticipleForm("undergone")),
            Verb.CreateIrregularVerb(new Text("understand"), new PastForm("understood"), new PastParticipleForm("understood")),
            Verb.CreateIrregularVerb(new Text("wake"), new PastForm("woke"), new PastParticipleForm("woken")),
            Verb.CreateIrregularVerb(new Text("wear"), new PastForm("wore"), new PastParticipleForm("worn")),
            Verb.CreateIrregularVerb(new Text("weep"), new PastForm("wept"), new PastParticipleForm("wept")),
            Verb.CreateIrregularVerb(new Text("wet"), new PastForm("wet"), new PastParticipleForm("wet")),
            Verb.CreateIrregularVerb(new Text("win"), new PastForm("won"), new PastParticipleForm("won")),
            Verb.CreateIrregularVerb(new Text("wind"), new PastForm("wound"), new PastParticipleForm("wound")),
            Verb.CreateIrregularVerb(new Text("withdraw"), new PastForm("withdrew"), new PastParticipleForm("withdrawn")),
            Verb.CreateIrregularVerb(new Text("wring"), new PastForm("wrung"), new PastParticipleForm("wrung")),
            Verb.CreateIrregularVerb(new Text("write"), new PastForm("wrote"), new PastParticipleForm("written")),
        ];

        return [..generatedVerbs.Select(x => new { Text = x.Text.Value, PastForm = x.PastForm.Value, PastParticipleForm = x.PastParticipleForm.Value, PresentParticipleForm = x.PresentParticipleForm.Value, ThirdPersonForm = x.ThirdPersonForm.Value, IsIrregularVerb = x.IsIrregularVerb.Value })];
    }

    private static void InsertQuestionWords(IDbConnection connection)
    {
        object[] questionWords = GetQuestionWords();

        const string questionWordsSql = """
                                        INSERT INTO public.question_words (text)
                                        VALUES (@Text);
                                        """;

        connection.Execute(questionWordsSql, questionWords);
    }

    private static object[] GetQuestionWords()
    {
        object[] questionWords =
        [
            new { Text = "what" },
            new { Text = "who" },
            new { Text = "whom" },
            new { Text = "whose" },
            new { Text = "where" },
            new { Text = "when" },
            new { Text = "why" },
            new { Text = "how" },
        ];

        return questionWords;
    }

    private static void InsertAdditionalForms(IDbConnection connection)
    {
        object[] additionalForms = GetAdditionalForms();

        const string additionalFormsSql = """
                                          INSERT INTO public.additional_forms (text, primary_verb_id)
                                          VALUES (@Text, @PrimaryVerbId);
                                          """;

        connection.Execute(additionalFormsSql, additionalForms);
    }

    private static void InsertPronouns(IDbConnection connection)
    {
        object[] pronouns = GetPronouns();

        const string pronounsSql = """
                                   INSERT INTO public.pronouns (text, type)
                                   VALUES (@Text, @Type);
                                   """;

        connection.Execute(pronounsSql, pronouns);
    }

    private static object[] GetPronouns()
    {
        object[] pronouns =
        [
            new { Text = "i", Type = PronounType.Subject },
            new { Text = "you", Type = PronounType.Subject },
            new { Text = "he", Type = PronounType.Subject },
            new { Text = "she", Type = PronounType.Subject },
            new { Text = "it", Type = PronounType.Subject },
            new { Text = "we", Type = PronounType.Subject },
            new { Text = "they", Type = PronounType.Subject },

            new { Text = "me", Type = PronounType.Obj },
            new { Text = "him", Type = PronounType.Obj },
            new { Text = "us", Type = PronounType.Obj },
            new { Text = "them", Type = PronounType.Obj },

            new { Text = "my", Type = PronounType.PossessiveAdjective },
            new { Text = "your", Type = PronounType.PossessiveAdjective },
            new { Text = "his", Type = PronounType.PossessiveAdjective },
            new { Text = "her", Type = PronounType.PossessiveAdjective },
            new { Text = "its", Type = PronounType.PossessiveAdjective },
            new { Text = "our", Type = PronounType.PossessiveAdjective },
            new { Text = "their", Type = PronounType.PossessiveAdjective },

            new { Text = "mine", Type = PronounType.Possessive },
            new { Text = "hers", Type = PronounType.Possessive },
            new { Text = "ours", Type = PronounType.Possessive },
            new { Text = "theirs", Type = PronounType.Possessive },

            new { Text = "myself", Type = PronounType.Reflexive },
            new { Text = "yourself", Type = PronounType.Reflexive },
            new { Text = "himself", Type = PronounType.Reflexive },
            new { Text = "herself", Type = PronounType.Reflexive },
            new { Text = "itself", Type = PronounType.Reflexive },
            new { Text = "ourselves", Type = PronounType.Reflexive },
            new { Text = "yourselves", Type = PronounType.Reflexive },
            new { Text = "themselves", Type = PronounType.Reflexive },

            new { Text = "this", Type = PronounType.Demonstrative },
            new { Text = "that", Type = PronounType.Demonstrative },
            new { Text = "these", Type = PronounType.Demonstrative },
            new { Text = "those", Type = PronounType.Demonstrative },
        ];

        return pronouns;
    }

    private static void InsertShortNegativeForms(IDbConnection connection)
    {
        object[] shortNegativeForms = GetShortNegativeForms();

        const string shortNegativeFormsSql = """
                                             INSERT INTO public.short_negative_forms (text, primary_verb_id)
                                             VALUES (@Text, @PrimaryVerbId);
                                             """;

        connection.Execute(shortNegativeFormsSql, shortNegativeForms);
    }

    private static void InsertFullNegativeForms(IDbConnection connection)
    {
        object[] fullNegativeForms = GetFullNegativeForms();

        const string fullNegativeFormsSql = """
                                            INSERT INTO public.full_negative_forms (text, primary_verb_id)
                                            VALUES (@Text, @PrimaryVerbId);
                                            """;

        connection.Execute(fullNegativeFormsSql, fullNegativeForms);
    }

    private static object[] GetFullNegativeForms()
    {
        object[] fullNegativeForms =
        [
            new { Text = "do not", PrimaryVerbId = 1 },
            new { Text = "did not", PrimaryVerbId = 1 },
            new { Text = "does not", PrimaryVerbId = 1 },
            new { Text = "have not", PrimaryVerbId = 2 },
            new { Text = "had not", PrimaryVerbId = 2 },
            new { Text = "has not", PrimaryVerbId = 2 },
            new { Text = "was not", PrimaryVerbId = 3 },
            new { Text = "were not", PrimaryVerbId = 3 },
            new { Text = "am not", PrimaryVerbId = 3 },
            new { Text = "is not", PrimaryVerbId = 3 },
            new { Text = "are not", PrimaryVerbId = 3 },
        ];

        return fullNegativeForms;
    }

    private static object[] GetShortNegativeForms()
    {
        object[] shortNegativeForms =
        [
            new { Text = "don't", PrimaryVerbId = 1 },
            new { Text = "didn't", PrimaryVerbId = 1 },
            new { Text = "doesn't", PrimaryVerbId = 1 },
            new { Text = "haven't", PrimaryVerbId = 2 },
            new { Text = "hadn't", PrimaryVerbId = 2 },
            new { Text = "hasn't", PrimaryVerbId = 2 },
            new { Text = "wasn't", PrimaryVerbId = 3 },
            new { Text = "weren't", PrimaryVerbId = 3 },
            new { Text = "am not", PrimaryVerbId = 3 },
            new { Text = "isn't", PrimaryVerbId = 3 },
            new { Text = "aren't", PrimaryVerbId = 3 },
        ];

        return shortNegativeForms;
    }

    private static object[] GetAdditionalForms()
    {
        object[] additionalForms =
        [
            new { Text = "were", PrimaryVerbId = 3 },
            new { Text = "am", PrimaryVerbId = 3 },
            new { Text = "are", PrimaryVerbId = 3 },
        ];

        return additionalForms;
    }

    private static void InsertPrimaryVerbs(IDbConnection connection)
    {
        object[] primaryVerbs = GetPrimaryVerbs();

        const string primaryVerbsSql = """
                                       INSERT INTO public.primary_verbs (id, text, past_form, past_participle_form, present_participle_form, third_person_form)
                                       VALUES (@Id, @Text, @PastForm, @PastParticipleForm, @PresentParticipleForm, @ThirdPersonForm);
                                       """;

        connection.Execute(primaryVerbsSql, primaryVerbs);
    }

    private static object[] GetPrimaryVerbs()
    {
        object[] primaryVerbs =
        [
            new { Id = 1, Text = "do", PastForm = "did", PastParticipleForm = "done", PresentParticipleForm = "doing", ThirdPersonForm = "does" },
            new { Id = 2, Text = "have", PastForm = "had", PastParticipleForm = "had", PresentParticipleForm = "having", ThirdPersonForm = "has" },
            new { Id = 3, Text = "be", PastForm = "was", PastParticipleForm = "been", PresentParticipleForm = "being", ThirdPersonForm = "is" },
        ];

        return primaryVerbs;
    }

    private static void InsertModalVerbs(IDbConnection connection)
    {
        object[] modalVerbs = GetModalVerbs();

        const string modalVerbsSql = """
                                     INSERT INTO public.modal_verbs (text, full_negative_form, short_negative_form)
                                     VALUES (@Text, @FullNegativeForm, @ShortNegativeForm);
                                     """;

        connection.Execute(modalVerbsSql, modalVerbs);
    }

    private static void InsertPrepositions(IDbConnection connection)
    {
        object[] prepositions = GetPrepositions();

        const string prepositionsSql = """
                                       INSERT INTO public.prepositions (text)
                                       VALUES (@Text);
                                       """;

        connection.Execute(prepositionsSql, prepositions);
    }

    private static object[] GetPrepositions()
    {
        object[] prepositions =
        [
            new { Text = "ago" },
            new { Text = "aboard" },
            new { Text = "about" },
            new { Text = "across" },
            new { Text = "after" },
            new { Text = "against" },
            new { Text = "along" },
            new { Text = "amid" },
            new { Text = "among" },
            new { Text = "anti" },
            new { Text = "around" },
            new { Text = "as" },
            new { Text = "at" },
            new { Text = "before" },
            new { Text = "behind" },
            new { Text = "beneath" },
            new { Text = "beside" },
            new { Text = "besides" },
            new { Text = "between" },
            new { Text = "beyond" },
            new { Text = "but" },
            new { Text = "by" },
            new { Text = "concerning" },
            new { Text = "considering" },
            new { Text = "despite" },
            new { Text = "down" },
            new { Text = "during" },
            new { Text = "except" },
            new { Text = "excepting" },
            new { Text = "excluding" },
            new { Text = "following" },
            new { Text = "for" },
            new { Text = "from" },
            new { Text = "in" },
            new { Text = "into" },
            new { Text = "minus" },
            new { Text = "near" },
            new { Text = "of" },
            new { Text = "off" },
            new { Text = "on" },
            new { Text = "onto" },
            new { Text = "opposite" },
            new { Text = "over" },
            new { Text = "past" },
            new { Text = "per" },
            new { Text = "plus" },
            new { Text = "regarding" },
            new { Text = "round" },
            new { Text = "save" },
            new { Text = "since" },
            new { Text = "than" },
            new { Text = "then" },
            new { Text = "through" },
            new { Text = "to" },
            new { Text = "toward" },
            new { Text = "towards" },
            new { Text = "under" },
            new { Text = "underneath" },
            new { Text = "unlike" },
            new { Text = "until" },
            new { Text = "up" },
            new { Text = "upon" },
            new { Text = "versus" },
            new { Text = "via" },
            new { Text = "with" },
            new { Text = "within" },
            new { Text = "without" },
        ];

        return prepositions;
    }

    private static void InsertNouns(IDbConnection connection)
    {
        object[] nouns = GetNouns();

        const string nounsSql = """
                                INSERT INTO public.nouns (text, plural_form, type)
                                VALUES (@Text, @PluralForm, @Type);
                                """;

        connection.Execute(nounsSql, nouns);
    }

    private static object[] GetNouns()
    {
        Noun[] generatedNouns =
        [
            Noun.Create(new Text("weekend"), NounType.RegularNoun),
            Noun.Create(new Text("time"), NounType.RegularNoun),
            Noun.Create(new Text("year"), NounType.RegularNoun),
            Noun.Create(new Text("way"), NounType.RegularNoun),
            Noun.Create(new Text("day"), NounType.RegularNoun),
            Noun.Create(new Text("thing"), NounType.RegularNoun),
            Noun.Create(new Text("life"), NounType.RegularNoun),
            Noun.Create(new Text("world"), NounType.RegularNoun),
            Noun.Create(new Text("school"), NounType.RegularNoun),
            Noun.Create(new Text("state"), NounType.RegularNoun),
            Noun.Create(new Text("family"), NounType.RegularNoun),
            Noun.Create(new Text("student"), NounType.RegularNoun),
            Noun.Create(new Text("group"), NounType.RegularNoun),
            Noun.Create(new Text("country"), NounType.RegularNoun),
            Noun.Create(new Text("problem"), NounType.RegularNoun),
            Noun.Create(new Text("hand"), NounType.RegularNoun),
            Noun.Create(new Text("part"), NounType.RegularNoun),
            Noun.Create(new Text("place"), NounType.RegularNoun),
            Noun.Create(new Text("case"), NounType.RegularNoun),
            Noun.Create(new Text("week"), NounType.RegularNoun),
            Noun.Create(new Text("company"), NounType.RegularNoun),
            Noun.Create(new Text("system"), NounType.RegularNoun),
            Noun.Create(new Text("program"), NounType.RegularNoun),
            Noun.Create(new Text("question"), NounType.RegularNoun),
            Noun.Create(new Text("government"), NounType.RegularNoun),
            Noun.Create(new Text("number"), NounType.RegularNoun),
            Noun.Create(new Text("point"), NounType.RegularNoun),
            Noun.Create(new Text("home"), NounType.RegularNoun),
            Noun.Create(new Text("water"), NounType.RegularNoun),
            Noun.Create(new Text("room"), NounType.RegularNoun),
            Noun.Create(new Text("mother"), NounType.RegularNoun),
            Noun.Create(new Text("area"), NounType.RegularNoun),
            Noun.Create(new Text("money"), NounType.RegularNoun),
            Noun.Create(new Text("story"), NounType.RegularNoun),
            Noun.Create(new Text("fact"), NounType.RegularNoun),
            Noun.Create(new Text("month"), NounType.RegularNoun),
            Noun.Create(new Text("book"), NounType.RegularNoun),
            Noun.Create(new Text("eye"), NounType.RegularNoun),
            Noun.Create(new Text("job"), NounType.RegularNoun),
            Noun.Create(new Text("word"), NounType.RegularNoun),
            Noun.Create(new Text("business"), NounType.RegularNoun),
            Noun.Create(new Text("issue"), NounType.RegularNoun),
            Noun.Create(new Text("side"), NounType.RegularNoun),
            Noun.Create(new Text("kind"), NounType.RegularNoun),
            Noun.Create(new Text("head"), NounType.RegularNoun),
            Noun.Create(new Text("house"), NounType.RegularNoun),
            Noun.Create(new Text("service"), NounType.RegularNoun),
            Noun.Create(new Text("friend"), NounType.RegularNoun),
            Noun.Create(new Text("father"), NounType.RegularNoun),
            Noun.Create(new Text("power"), NounType.RegularNoun),
            Noun.Create(new Text("hour"), NounType.RegularNoun),
            Noun.Create(new Text("game"), NounType.RegularNoun),
            Noun.Create(new Text("line"), NounType.RegularNoun),
            Noun.Create(new Text("end"), NounType.RegularNoun),
            Noun.Create(new Text("member"), NounType.RegularNoun),
            Noun.Create(new Text("law"), NounType.RegularNoun),
            Noun.Create(new Text("car"), NounType.RegularNoun),
            Noun.Create(new Text("city"), NounType.RegularNoun),
            Noun.Create(new Text("community"), NounType.RegularNoun),
            Noun.Create(new Text("name"), NounType.RegularNoun),
            Noun.Create(new Text("president"), NounType.RegularNoun),
            Noun.Create(new Text("team"), NounType.RegularNoun),
            Noun.Create(new Text("minute"), NounType.RegularNoun),
            Noun.Create(new Text("idea"), NounType.RegularNoun),
            Noun.Create(new Text("kid"), NounType.RegularNoun),
            Noun.Create(new Text("body"), NounType.RegularNoun),
            Noun.Create(new Text("information"), NounType.RegularNoun),
            Noun.Create(new Text("parent"), NounType.RegularNoun),
            Noun.Create(new Text("face"), NounType.RegularNoun),
            Noun.Create(new Text("others"), NounType.RegularNoun),
            Noun.Create(new Text("level"), NounType.RegularNoun),
            Noun.Create(new Text("office"), NounType.RegularNoun),
            Noun.Create(new Text("door"), NounType.RegularNoun),
            Noun.Create(new Text("health"), NounType.RegularNoun),
            Noun.Create(new Text("art"), NounType.RegularNoun),
            Noun.Create(new Text("war"), NounType.RegularNoun),
            Noun.Create(new Text("history"), NounType.RegularNoun),
            Noun.Create(new Text("party"), NounType.RegularNoun),
            Noun.Create(new Text("result"), NounType.RegularNoun),
            Noun.Create(new Text("change"), NounType.RegularNoun),
            Noun.Create(new Text("reason"), NounType.RegularNoun),
            Noun.Create(new Text("research"), NounType.RegularNoun),
            Noun.Create(new Text("girl"), NounType.RegularNoun),
            Noun.Create(new Text("guy"), NounType.RegularNoun),
            Noun.Create(new Text("moment"), NounType.RegularNoun),
            Noun.Create(new Text("air"), NounType.RegularNoun),
            Noun.Create(new Text("force"), NounType.RegularNoun),
            Noun.Create(new Text("education"), NounType.RegularNoun),
            Noun.Create(new Text("accountant"), NounType.Occupation),
            Noun.Create(new Text("actor"), NounType.Occupation),
            Noun.Create(new Text("actress"), NounType.Occupation),
            Noun.Create(new Text("architect"), NounType.Occupation),
            Noun.Create(new Text("astronomer"), NounType.Occupation),
            Noun.Create(new Text("author"), NounType.Occupation),
            Noun.Create(new Text("baker"), NounType.Occupation),
            Noun.Create(new Text("bricklayer"), NounType.Occupation),
            Noun.Create(new Text("bus"), NounType.Occupation),
            Noun.Create(new Text("driver"), NounType.Occupation),
            Noun.Create(new Text("butcher"), NounType.Occupation),
            Noun.Create(new Text("carpenter"), NounType.Occupation),
            Noun.Create(new Text("chef"), NounType.Occupation),
            Noun.Create(new Text("cleaner"), NounType.Occupation),
            Noun.Create(new Text("courier"), NounType.Occupation),
            Noun.Create(new Text("dentist"), NounType.Occupation),
            Noun.Create(new Text("designer"), NounType.Occupation),
            Noun.Create(new Text("doctor"), NounType.Occupation),
            Noun.Create(new Text("dustman"), NounType.Occupation),
            Noun.Create(new Text("electrician"), NounType.Occupation),
            Noun.Create(new Text("engineer"), NounType.Occupation),
            Noun.Create(new Text("farmer"), NounType.Occupation),
            Noun.Create(new Text("fireman"), NounType.Occupation),
            Noun.Create(new Text("fisherman"), NounType.Occupation),
            Noun.Create(new Text("florist"), NounType.Occupation),
            Noun.Create(new Text("gardener"), NounType.Occupation),
            Noun.Create(new Text("hairdresser"), NounType.Occupation),
            Noun.Create(new Text("journalist"), NounType.Occupation),
            Noun.Create(new Text("judge"), NounType.Occupation),
            Noun.Create(new Text("lawyer"), NounType.Occupation),
            Noun.Create(new Text("lecturer"), NounType.Occupation),
            Noun.Create(new Text("librarian"), NounType.Occupation),
            Noun.Create(new Text("lifeguard"), NounType.Occupation),
            Noun.Create(new Text("mechanic"), NounType.Occupation),
            Noun.Create(new Text("model"), NounType.Occupation),
            Noun.Create(new Text("newsreader"), NounType.Occupation),
            Noun.Create(new Text("nurse"), NounType.Occupation),
            Noun.Create(new Text("optician"), NounType.Occupation),
            Noun.Create(new Text("painter"), NounType.Occupation),
            Noun.Create(new Text("pharmacist"), NounType.Occupation),
            Noun.Create(new Text("photographer"), NounType.Occupation),
            Noun.Create(new Text("pilot"), NounType.Occupation),
            Noun.Create(new Text("plumber"), NounType.Occupation),
            Noun.Create(new Text("politician"), NounType.Occupation),
            Noun.Create(new Text("policeman"), NounType.Occupation),
            Noun.Create(new Text("policewoman"), NounType.Occupation),
            Noun.Create(new Text("postman"), NounType.Occupation),
            Noun.Create(new Text("receptionist"), NounType.Occupation),
            Noun.Create(new Text("scientist"), NounType.Occupation),
            Noun.Create(new Text("secretary"), NounType.Occupation),
            Noun.Create(new Text("soldier"), NounType.Occupation),
            Noun.Create(new Text("tailor"), NounType.Occupation),
            Noun.Create(new Text("teacher"), NounType.Occupation),
            Noun.Create(new Text("translator"), NounType.Occupation),
            Noun.Create(new Text("waiter"), NounType.Occupation),
            Noun.Create(new Text("waitress"), NounType.Occupation),
            Noun.Create(new Text("morning"), NounType.DayPart),
            Noun.Create(new Text("afternoon"), NounType.DayPart),
            Noun.Create(new Text("evening"), NounType.DayPart),
            Noun.Create(new Text("night"), NounType.DayPart),
            Noun.Create(new Text("winter"), NounType.YearSeason),
            Noun.Create(new Text("spring"), NounType.YearSeason),
            Noun.Create(new Text("summer"), NounType.YearSeason),
            Noun.Create(new Text("autumn"), NounType.YearSeason),
        ];

        object[] nouns =
        [
            new { Text = "addendum", PluralForm = "addenda", Type = NounType.IrregularNoun },
            new { Text = "aircraft", PluralForm = "aircraft", Type = NounType.IrregularNoun },
            new { Text = "alumna", PluralForm = "alumnae", Type = NounType.IrregularNoun },
            new { Text = "alumnus", PluralForm = "alumni", Type = NounType.IrregularNoun },
            new { Text = "analysis", PluralForm = "analyses", Type = NounType.IrregularNoun },
            new { Text = "antenna", PluralForm = "antennae", Type = NounType.IrregularNoun },
            new { Text = "antithesis", PluralForm = "antitheses", Type = NounType.IrregularNoun },
            new { Text = "apex", PluralForm = "apices", Type = NounType.IrregularNoun },
            new { Text = "appendix", PluralForm = "appendices", Type = NounType.IrregularNoun },
            new { Text = "axis", PluralForm = "axes", Type = NounType.IrregularNoun },
            new { Text = "bacillus", PluralForm = "bacilli", Type = NounType.IrregularNoun },
            new { Text = "bacterium", PluralForm = "bacteria", Type = NounType.IrregularNoun },
            new { Text = "basis", PluralForm = "bases", Type = NounType.IrregularNoun },
            new { Text = "beau", PluralForm = "beaux", Type = NounType.IrregularNoun },
            new { Text = "bison", PluralForm = "bison", Type = NounType.IrregularNoun },
            new { Text = "bureau", PluralForm = "bureaux", Type = NounType.IrregularNoun },
            new { Text = "cactus", PluralForm = "cacti", Type = NounType.IrregularNoun },
            new { Text = "chateau", PluralForm = "chateaux", Type = NounType.IrregularNoun },
            new { Text = "child", PluralForm = "children", Type = NounType.IrregularNoun },
            new { Text = "codex", PluralForm = "codices", Type = NounType.IrregularNoun },
            new { Text = "concerto", PluralForm = "concerti", Type = NounType.IrregularNoun },
            new { Text = "corpus", PluralForm = "corpora", Type = NounType.IrregularNoun },
            new { Text = "crisis", PluralForm = "crises", Type = NounType.IrregularNoun },
            new { Text = "criterion", PluralForm = "criteria", Type = NounType.IrregularNoun },
            new { Text = "curriculum", PluralForm = "curricula", Type = NounType.IrregularNoun },
            new { Text = "datum", PluralForm = "data", Type = NounType.IrregularNoun },
            new { Text = "deer", PluralForm = "deer", Type = NounType.IrregularNoun },
            new { Text = "diagnosis", PluralForm = "diagnoses", Type = NounType.IrregularNoun },
            new { Text = "die", PluralForm = "dice", Type = NounType.IrregularNoun },
            new { Text = "dwarf", PluralForm = "dwarves", Type = NounType.IrregularNoun },
            new { Text = "ellipsis", PluralForm = "ellipses", Type = NounType.IrregularNoun },
            new { Text = "erratum", PluralForm = "errata", Type = NounType.IrregularNoun },
            new { Text = "fez", PluralForm = "fezzes", Type = NounType.IrregularNoun },
            new { Text = "focus", PluralForm = "foci", Type = NounType.IrregularNoun },
            new { Text = "foot", PluralForm = "feet", Type = NounType.IrregularNoun },
            new { Text = "formula", PluralForm = "formulae", Type = NounType.IrregularNoun },
            new { Text = "fungus", PluralForm = "fungi", Type = NounType.IrregularNoun },
            new { Text = "genus", PluralForm = "genera", Type = NounType.IrregularNoun },
            new { Text = "goose", PluralForm = "geese", Type = NounType.IrregularNoun },
            new { Text = "graffito", PluralForm = "graffiti", Type = NounType.IrregularNoun },
            new { Text = "grouse", PluralForm = "grouse", Type = NounType.IrregularNoun },
            new { Text = "half", PluralForm = "halves", Type = NounType.IrregularNoun },
            new { Text = "hoof", PluralForm = "hooves", Type = NounType.IrregularNoun },
            new { Text = "hypothesis", PluralForm = "hypotheses", Type = NounType.IrregularNoun },
            new { Text = "index", PluralForm = "indices", Type = NounType.IrregularNoun },
            new { Text = "larva", PluralForm = "larvae", Type = NounType.IrregularNoun },
            new { Text = "libretto", PluralForm = "libretti", Type = NounType.IrregularNoun },
            new { Text = "loaf", PluralForm = "loaves", Type = NounType.IrregularNoun },
            new { Text = "locus", PluralForm = "loci", Type = NounType.IrregularNoun },
            new { Text = "louse", PluralForm = "lice", Type = NounType.IrregularNoun },
            new { Text = "man", PluralForm = "men", Type = NounType.IrregularNoun },
            new { Text = "matrix", PluralForm = "matrices", Type = NounType.IrregularNoun },
            new { Text = "medium", PluralForm = "media", Type = NounType.IrregularNoun },
            new { Text = "memorandum", PluralForm = "memoranda", Type = NounType.IrregularNoun },
            new { Text = "minutia", PluralForm = "minutiae", Type = NounType.IrregularNoun },
            new { Text = "moose", PluralForm = "moose", Type = NounType.IrregularNoun },
            new { Text = "mouse", PluralForm = "mice", Type = NounType.IrregularNoun },
            new { Text = "nebula", PluralForm = "nebulae", Type = NounType.IrregularNoun },
            new { Text = "nucleus", PluralForm = "nuclei", Type = NounType.IrregularNoun },
            new { Text = "oasis", PluralForm = "oases", Type = NounType.IrregularNoun },
            new { Text = "opus", PluralForm = "opera", Type = NounType.IrregularNoun },
            new { Text = "ovum", PluralForm = "ova", Type = NounType.IrregularNoun },
            new { Text = "ox", PluralForm = "oxen", Type = NounType.IrregularNoun },
            new { Text = "parenthesis", PluralForm = "parentheses", Type = NounType.IrregularNoun },
            new { Text = "phenomenon", PluralForm = "phenomena", Type = NounType.IrregularNoun },
            new { Text = "phylum", PluralForm = "phyla", Type = NounType.IrregularNoun },
            new { Text = "quiz", PluralForm = "quizzes", Type = NounType.IrregularNoun },
            new { Text = "radius", PluralForm = "radii", Type = NounType.IrregularNoun },
            new { Text = "referendum", PluralForm = "referenda", Type = NounType.IrregularNoun },
            new { Text = "salmon", PluralForm = "salmon", Type = NounType.IrregularNoun },
            new { Text = "scarf", PluralForm = "scarves", Type = NounType.IrregularNoun },
            new { Text = "self", PluralForm = "selves", Type = NounType.IrregularNoun },
            new { Text = "series", PluralForm = "series", Type = NounType.IrregularNoun },
            new { Text = "sheep", PluralForm = "sheep", Type = NounType.IrregularNoun },
            new { Text = "species", PluralForm = "species", Type = NounType.IrregularNoun },
            new { Text = "stimulus", PluralForm = "stimuli", Type = NounType.IrregularNoun },
            new { Text = "stratum", PluralForm = "strata", Type = NounType.IrregularNoun },
            new { Text = "swine", PluralForm = "swine", Type = NounType.IrregularNoun },
            new { Text = "syllabus", PluralForm = "syllabi", Type = NounType.IrregularNoun },
            new { Text = "symposium", PluralForm = "symposia", Type = NounType.IrregularNoun },
            new { Text = "synopsis", PluralForm = "synopses", Type = NounType.IrregularNoun },
            new { Text = "tableau", PluralForm = "tableaux", Type = NounType.IrregularNoun },
            new { Text = "thesis", PluralForm = "theses", Type = NounType.IrregularNoun },
            new { Text = "thief", PluralForm = "thieves", Type = NounType.IrregularNoun },
            new { Text = "tooth", PluralForm = "teeth", Type = NounType.IrregularNoun },
            new { Text = "vertebra", PluralForm = "vertebrae", Type = NounType.IrregularNoun },
            new { Text = "vertex", PluralForm = "vertices", Type = NounType.IrregularNoun },
            new { Text = "vita", PluralForm = "vitae", Type = NounType.IrregularNoun },
            new { Text = "vortex", PluralForm = "vortices", Type = NounType.IrregularNoun },
            new { Text = "wharf", PluralForm = "wharves", Type = NounType.IrregularNoun },
            new { Text = "wife", PluralForm = "wives", Type = NounType.IrregularNoun },
            new { Text = "wolf", PluralForm = "wolves", Type = NounType.IrregularNoun },
            new { Text = "woman", PluralForm = "women", Type = NounType.IrregularNoun },
            new { Text = "person", PluralForm = "people", Type = NounType.IrregularNoun },
        ];

        return
        [
            ..nouns,
            ..generatedNouns.Select(x => new { Text = x.Text.Value, PluralForm = x.PluralForm.Value, x.Type })
        ];
    }

    private static object[] GetModalVerbs()
    {
        object[] modalVerbs =
        [
            new { Text = "can", FullNegativeForm = "can not", ShortNegativeForm = "can't" },
            new { Text = "could", FullNegativeForm = "could not", ShortNegativeForm = "couldn't" },
            new { Text = "may", FullNegativeForm = "may not", ShortNegativeForm = "may not" },
            new { Text = "might", FullNegativeForm = "might not", ShortNegativeForm = "mightn't" },
            new { Text = "will", FullNegativeForm = "will not", ShortNegativeForm = "won't" },
            new { Text = "shall", FullNegativeForm = "shall not", ShortNegativeForm = "shan't" },
            new { Text = "would", FullNegativeForm = "would not", ShortNegativeForm = "wouldn't" },
            new { Text = "should", FullNegativeForm = "should not", ShortNegativeForm = "shouldn't" },
            new { Text = "must", FullNegativeForm = "must not", ShortNegativeForm = "mustn't" },
        ];

        return modalVerbs;
    }

    private static void InsertLanguages(IDbConnection connection)
    {
        object[] languages = GetLanguages();

        const string languagesSql = """
                                    INSERT INTO public.languages (text)
                                    VALUES (@Text);
                                    """;

        connection.Execute(languagesSql, languages);
    }

    private static void InsertLetterNumbers(IDbConnection connection)
    {
        object[] letterNumbers = GetLetterNumbers();

        const string letterNumbersSql = """
                                        INSERT INTO public.letter_numbers (text, number)
                                        VALUES (@Text, @Number);
                                        """;

        connection.Execute(letterNumbersSql, letterNumbers);
    }

    private static object[] GetLetterNumbers()
    {
        object[] letterNumbers =
        [
            new { Text = "one", Number = 1 },
            new { Text = "two", Number = 2 },
            new { Text = "three", Number = 3 },
            new { Text = "four", Number = 4 },
            new { Text = "five", Number = 5 },
            new { Text = "six", Number = 6 },
            new { Text = "seven", Number = 7 },
            new { Text = "eight", Number = 8 },
            new { Text = "nine", Number = 9 },
            new { Text = "ten", Number = 10 },
            new { Text = "eleven", Number = 11 },
            new { Text = "twelve", Number = 12 },
            new { Text = "thirteen", Number = 13 },
            new { Text = "fourteen", Number = 14 },
            new { Text = "fifteen", Number = 15 },
            new { Text = "sixteen", Number = 16 },
            new { Text = "seventeen", Number = 17 },
            new { Text = "eighteen", Number = 18 },
            new { Text = "nineteen", Number = 19 },
            new { Text = "twenty", Number = 20 },
            new { Text = "twenty-one", Number = 21 },
            new { Text = "twenty-two", Number = 22 },
            new { Text = "twenty-three", Number = 23 },
            new { Text = "twenty-four", Number = 24 },
            new { Text = "twenty-five", Number = 25 },
            new { Text = "twenty-six", Number = 26 },
            new { Text = "twenty-seven", Number = 27 },
            new { Text = "twenty-eight", Number = 28 },
            new { Text = "twenty-nine", Number = 29 },
            new { Text = "thirty", Number = 30 },
            new { Text = "thirty-one", Number = 31 },
            new { Text = "thirty-two", Number = 32 },
            new { Text = "thirty-three", Number = 33 },
            new { Text = "thirty-four", Number = 34 },
            new { Text = "thirty-five", Number = 35 },
            new { Text = "thirty-six", Number = 36 },
            new { Text = "thirty-seven", Number = 37 },
            new { Text = "thirty-eight", Number = 38 },
            new { Text = "thirty-nine", Number = 39 },
            new { Text = "forty", Number = 40 },
            new { Text = "forty-one", Number = 41 },
            new { Text = "forty-two", Number = 42 },
            new { Text = "forty-three", Number = 43 },
            new { Text = "forty-four", Number = 44 },
            new { Text = "forty-five", Number = 45 },
            new { Text = "forty-six", Number = 46 },
            new { Text = "forty-seven", Number = 47 },
            new { Text = "forty-eight", Number = 48 },
            new { Text = "forty-nine", Number = 49 },
            new { Text = "fifty", Number = 50 },
            new { Text = "fifty-one", Number = 51 },
            new { Text = "fifty-two", Number = 52 },
            new { Text = "fifty-three", Number = 53 },
            new { Text = "fifty-four", Number = 54 },
            new { Text = "fifty-five", Number = 55 },
            new { Text = "fifty-six", Number = 56 },
            new { Text = "fifty-seven", Number = 57 },
            new { Text = "fifty-eight", Number = 58 },
            new { Text = "fifty-nine", Number = 59 },
            new { Text = "sixty", Number = 60 },
            new { Text = "sixty-one", Number = 61 },
            new { Text = "sixty-two", Number = 62 },
            new { Text = "sixty-three", Number = 63 },
            new { Text = "sixty-four", Number = 64 },
            new { Text = "sixty-five", Number = 65 },
            new { Text = "sixty-six", Number = 66 },
            new { Text = "sixty-seven", Number = 67 },
            new { Text = "sixty-eight", Number = 68 },
            new { Text = "sixty-nine", Number = 69 },
            new { Text = "seventy", Number = 70 },
            new { Text = "seventy-one", Number = 71 },
            new { Text = "seventy-two", Number = 72 },
            new { Text = "seventy-three", Number = 73 },
            new { Text = "seventy-four", Number = 74 },
            new { Text = "seventy-five", Number = 75 },
            new { Text = "seventy-six", Number = 76 },
            new { Text = "seventy-seven", Number = 77 },
            new { Text = "seventy-eight", Number = 78 },
            new { Text = "seventy-nine", Number = 79 },
            new { Text = "eighty", Number = 80 },
            new { Text = "eighty-one", Number = 81 },
            new { Text = "eighty-two", Number = 82 },
            new { Text = "eighty-three", Number = 83 },
            new { Text = "eighty-four", Number = 84 },
            new { Text = "eighty-five", Number = 85 },
            new { Text = "eighty-six", Number = 86 },
            new { Text = "eighty-seven", Number = 87 },
            new { Text = "eighty-eight", Number = 88 },
            new { Text = "eighty-nine", Number = 89 },
            new { Text = "ninety", Number = 90 },
            new { Text = "ninety-one", Number = 91 },
            new { Text = "ninety-two", Number = 92 },
            new { Text = "ninety-three", Number = 93 },
            new { Text = "ninety-four", Number = 94 },
            new { Text = "ninety-five", Number = 95 },
            new { Text = "ninety-six", Number = 96 },
            new { Text = "ninety-seven", Number = 97 },
            new { Text = "ninety-eight", Number = 98 },
            new { Text = "ninety-nine", Number = 99 },
        ];

        return letterNumbers;
    }

    private static object[] GetLanguages()
    {
        object[] languages =
        [
            new { Text = "mandarin chinese" },
            new { Text = "spanish" },
            new { Text = "english" },
            new { Text = "hindi" },
            new { Text = "bengali" },
            new { Text = "portuguese" },
            new { Text = "russian" },
            new { Text = "japanese" },
            new { Text = "western punjabi" },
            new { Text = "marathi" },
            new { Text = "telugu" },
            new { Text = "wu chinese" },
            new { Text = "turkish" },
            new { Text = "korean" },
            new { Text = "french" },
            new { Text = "german" },
            new { Text = "vietnamese" },
            new { Text = "tamil" },
            new { Text = "yue chinese" },
            new { Text = "urdu" },
            new { Text = "javanese" },
            new { Text = "italian" },
            new { Text = "egyptian arabic" },
            new { Text = "gujarati" },
            new { Text = "iranian persian" },
            new { Text = "bhojpuri" },
            new { Text = "southern min" },
            new { Text = "hakka" },
            new { Text = "jin chinese" },
            new { Text = "hausa" },
            new { Text = "kannada" },
            new { Text = "indonesian" },
            new { Text = "polish" },
            new { Text = "yoruba" },
            new { Text = "xiang chinese" },
            new { Text = "malayalam" },
            new { Text = "odia" },
            new { Text = "maithili" },
            new { Text = "burmese" },
            new { Text = "eastern punjabi" },
            new { Text = "sunda" },
            new { Text = "sudanese arabic" },
            new { Text = "algerian arabic" },
            new { Text = "moroccan arabic" },
            new { Text = "ukrainian" },
            new { Text = "igbo" },
            new { Text = "northern uzbek" },
            new { Text = "sindhi" },
            new { Text = "north levantine arabic" },
            new { Text = "romanian" },
            new { Text = "tagalog" },
            new { Text = "dutch" },
            new { Text = "sa?idi arabic" },
            new { Text = "gan chinese" },
            new { Text = "amharic" },
            new { Text = "northern pashto" },
            new { Text = "magahi" },
            new { Text = "thai" },
            new { Text = "saraiki" },
            new { Text = "khmer" },
            new { Text = "chhattisgarhi" },
            new { Text = "somali" },
            new { Text = "malaysian" },
            new { Text = "cebuano" },
            new { Text = "nepali" },
            new { Text = "mesopotamian arabic" },
            new { Text = "assamese" },
            new { Text = "sinhalese" },
            new { Text = "northern kurdish" },
            new { Text = "hejazi arabic" },
            new { Text = "nigerian fulfulde" },
            new { Text = "bavarian" },
            new { Text = "south azerbaijani" },
            new { Text = "greek" },
            new { Text = "chittagonian" },
            new { Text = "kazakh" },
            new { Text = "deccan" },
            new { Text = "hungarian" },
            new { Text = "kinyarwanda" },
            new { Text = "zulu" },
            new { Text = "south levantine arabic" },
            new { Text = "tunisian arabic" },
            new { Text = "sanaani spoken arabic" },
            new { Text = "northern min" },
            new { Text = "southern pashto" },
            new { Text = "rundi" },
            new { Text = "czech" },
            new { Text = "ta?izzi-adeni arabic" },
            new { Text = "uyghur" },
            new { Text = "eastern min" },
            new { Text = "sylheti" },
        ];

        return languages;
    }

    private static void InsertDeterminers(IDbConnection connection)
    {
        object[] determiners = GetDeterminers();

        const string determinersSql = """
                                      INSERT INTO public.determiners (text)
                                      VALUES (@Text);
                                      """;

        connection.Execute(determinersSql, determiners);
    }

    private static object[] GetDeterminers()
    {
        object[] determiners =
        [
            new { Text = "the" },
            new { Text = "a" },
            new { Text = "an" },
        ];

        return determiners;
    }

    private static void InsertComparisonAdjectives(IDbConnection connection)
    {
        object[] comparisonAdjectives = GetComparisonAdjectives();

        const string comparisionAdjectivesSql = """
                                                INSERT INTO public.comparison_adjectives (text, comparative_form, superlative_form)
                                                VALUES (@Text, @ComparativeForm, @SuperlativeForm);
                                                """;

        connection.Execute(comparisionAdjectivesSql, comparisonAdjectives);
    }

    private static void InsertCompounds(IDbConnection connection)
    {
        object[] compounds = GetCompounds();

        const string compoundsSql = """
                                    INSERT INTO public.compounds (text, type)
                                    VALUES (@Text, @Type);
                                    """;

        connection.Execute(compoundsSql, compounds);
    }

    private static object[] GetCompounds()
    {
        object[] compounds =
        [
            new { Text = "something", Type = CompoundType.Some },
            new { Text = "somebody", Type = CompoundType.Some },
            new { Text = "someone", Type = CompoundType.Some },
            new { Text = "somewhere", Type = CompoundType.Some },
            new { Text = "sometimes", Type = CompoundType.Some },

            new { Text = "anything", Type = CompoundType.Any },
            new { Text = "anybody", Type = CompoundType.Any },
            new { Text = "anyone", Type = CompoundType.Any },
            new { Text = "anywhere", Type = CompoundType.Any },

            new { Text = "everything", Type = CompoundType.Every },
            new { Text = "everybody", Type = CompoundType.Every },
            new { Text = "everyone", Type = CompoundType.Every },
            new { Text = "everywhere", Type = CompoundType.Every },

            new { Text = "nothing", Type = CompoundType.No },
            new { Text = "nobody", Type = CompoundType.No },
            new { Text = "nowhere", Type = CompoundType.No },
        ];

        return compounds;
    }

    private static object[] GetComparisonAdjectives()
    {
        ComparisonAdjective[] generatedComparisonAdjectives =
        [
            ComparisonAdjective.Create(new Text("old"), new SyllablesCount(1)),
            ComparisonAdjective.Create(new Text("long"), new SyllablesCount(1)),
            ComparisonAdjective.Create(new Text("nice"), new SyllablesCount(1)),
            ComparisonAdjective.Create(new Text("large"), new SyllablesCount(1)),
            ComparisonAdjective.Create(new Text("big"), new SyllablesCount(1)),
            ComparisonAdjective.Create(new Text("fat"), new SyllablesCount(1)),
            ComparisonAdjective.Create(new Text("happy"), new SyllablesCount(2)),
            ComparisonAdjective.Create(new Text("silly"), new SyllablesCount(2)),
        ];

        object[] comparisonAdjectives =
        [
            new { Text = "good", ComparativeForm = "good", SuperlativeForm = "better" },
            new { Text = "bad", ComparativeForm = "bad", SuperlativeForm = "worse" },
            new { Text = "far", ComparativeForm = "far", SuperlativeForm = "farther" },
            new { Text = "little", ComparativeForm = "little", SuperlativeForm = "less" },
        ];

        return
        [
            .. comparisonAdjectives,
            .. generatedComparisonAdjectives.Select(x => new { Text = x.Text.Value, ComparativeForm = x.ComparativeForm.Value, SuperlativeForm = x.SuperlativeForm.Value }),
        ];
    }

    private static void InsertAdjectives(IDbConnection connection)
    {
        object[] adjectives = GetAdjectives();

        const string adjectivesSql = """
                                     INSERT INTO public.adjectives (text)
                                     VALUES (@Text);
                                     """;

        connection.Execute(adjectivesSql, adjectives);
    }

    private static void InsertAdverbs(IDbConnection connection)
    {
        object[] adverbs = GetAdverbs();

        const string adverbsSql = """
                                  INSERT INTO public.adverbs (text, type)
                                  VALUES (@Text, @Type);
                                  """;

        connection.Execute(adverbsSql, adverbs);
    }

    private static object[] GetAdverbs()
    {
        object[] adverbs =
        [
            new { Text = "again", Type = AdverbType.Frequency },
            new { Text = "always", Type = AdverbType.Frequency },
            new { Text = "every", Type = AdverbType.Frequency },
            new { Text = "never", Type = AdverbType.Frequency },
            new { Text = "normally", Type = AdverbType.Frequency },
            new { Text = "rarely", Type = AdverbType.Frequency },
            new { Text = "seldom", Type = AdverbType.Frequency },
            new { Text = "usually", Type = AdverbType.Frequency },

            new { Text = "literally", Type = AdverbType.Intensifier },
            new { Text = "simply", Type = AdverbType.Intensifier },
            new { Text = "really", Type = AdverbType.Intensifier },
            new { Text = "sure", Type = AdverbType.Intensifier },
            new { Text = "completely", Type = AdverbType.Intensifier },
            new { Text = "heartily", Type = AdverbType.Intensifier },
            new { Text = "totally", Type = AdverbType.Intensifier },
            new { Text = "absolutely", Type = AdverbType.Intensifier },
            new { Text = "somewhat", Type = AdverbType.Intensifier },
            new { Text = "mildly", Type = AdverbType.Intensifier },

            new { Text = "beautifully", Type = AdverbType.Manner },
            new { Text = "generously", Type = AdverbType.Manner },
            new { Text = "happily", Type = AdverbType.Manner },
            new { Text = "neatly", Type = AdverbType.Manner },
            new { Text = "patiently", Type = AdverbType.Manner },
            new { Text = "softly", Type = AdverbType.Manner },
            new { Text = "quickly", Type = AdverbType.Manner },
            new { Text = "well", Type = AdverbType.Manner },
            new { Text = "finally", Type = AdverbType.Manner },

            new { Text = "briskly", Type = AdverbType.TellHowItHappened },
            new { Text = "brutally", Type = AdverbType.TellHowItHappened },
            new { Text = "cheerfully", Type = AdverbType.TellHowItHappened },
            new { Text = "expertly", Type = AdverbType.TellHowItHappened },
            new { Text = "randomly", Type = AdverbType.TellHowItHappened },
            new { Text = "sloppily", Type = AdverbType.TellHowItHappened },
            new { Text = "uneasily", Type = AdverbType.TellHowItHappened },
            new { Text = "weirdly", Type = AdverbType.TellHowItHappened },
            new { Text = "wholeheartedly", Type = AdverbType.TellHowItHappened },
            new { Text = "wickedly", Type = AdverbType.TellHowItHappened },

            new { Text = "here", Type = AdverbType.TellWhereItHappened },
            new { Text = "there", Type = AdverbType.TellWhereItHappened },
            new { Text = "downstairs", Type = AdverbType.TellWhereItHappened },
            new { Text = "upstairs", Type = AdverbType.TellWhereItHappened },
            new { Text = "inside", Type = AdverbType.TellWhereItHappened },
            new { Text = "outside", Type = AdverbType.TellWhereItHappened },
            new { Text = "underground", Type = AdverbType.TellWhereItHappened },
            new { Text = "above", Type = AdverbType.TellWhereItHappened },
            new { Text = "back", Type = AdverbType.TellWhereItHappened },
            new { Text = "below", Type = AdverbType.TellWhereItHappened },
            new { Text = "out", Type = AdverbType.TellWhereItHappened },

            new { Text = "early", Type = AdverbType.TellWhenItHappened },
            new { Text = "first", Type = AdverbType.TellWhenItHappened },
            new { Text = "last", Type = AdverbType.TellWhenItHappened },
            new { Text = "later", Type = AdverbType.TellWhenItHappened },
            new { Text = "now", Type = AdverbType.TellWhenItHappened },
            new { Text = "regularly", Type = AdverbType.TellWhenItHappened },
            new { Text = "today", Type = AdverbType.TellWhenItHappened },
            new { Text = "tonight", Type = AdverbType.TellWhenItHappened },
            new { Text = "noon", Type = AdverbType.TellWhenItHappened },
            new { Text = "tomorrow", Type = AdverbType.TellWhenItHappened },
            new { Text = "yesterday", Type = AdverbType.TellWhenItHappened },
            new { Text = "already", Type = AdverbType.TellWhenItHappened },
            new { Text = "yet", Type = AdverbType.TellWhenItHappened },
            new { Text = "immediately", Type = AdverbType.TellWhenItHappened },
            new { Text = "lately", Type = AdverbType.TellWhenItHappened },
            new { Text = "recently", Type = AdverbType.TellWhenItHappened },
            new { Text = "soon", Type = AdverbType.TellWhenItHappened },

            new { Text = "almost", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "also", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "enough", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "only", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "quite", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "rather", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "so", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "too", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "very", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "hardly", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "just", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "nearly", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "more", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "anymore", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "much", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "some", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "lot", Type = AdverbType.TellTheExtentOfTheAction },
            new { Text = "no", Type = AdverbType.TellTheExtentOfTheAction },
        ];

        return adverbs;
    }

    private static void InsertWords(IDbConnection connection)
    {
        object[] words = GetWords();

        const string wordsSql = """
                                INSERT INTO public.words (number, text, type, objective_id)
                                VALUES (@Number, @Text, @Type, @ObjectiveId);
                                """;

        connection.Execute(wordsSql, words);
    }

    private static void InsertObjectiveQuestIds(IDbConnection connection)
    {
        object[] questObjectives = GetQuestObjectives();

        const string questObjectivesSql = """
                                          INSERT INTO public.objective_quest_ids (quest_id, objective_id)
                                          VALUES (@QuestId, @ObjectiveId);
                                          """;

        connection.Execute(questObjectivesSql, questObjectives);
    }

    private static void InsertObjectives(IDbConnection connection)
    {
        object[] objectives = GetObjectives();

        const string objectivesSql = """
                                    INSERT INTO public.objectives (id, rus_phrase)
                                    VALUES (@Id, @RusPhrase);
                                    """;

        connection.Execute(objectivesSql, objectives);
    }

    private static void InsertQuests(IDbConnection connection)
    {
        object[] quests = GetQuests();

        const string questsSql = """
                                  INSERT INTO public.quests (id, name)
                                  VALUES (@Id, @Name);
                                  """;

        connection.Execute(questsSql, quests);
    }

    private static object[] GetAdjectives()
    {
        object[] adjectives =
        [
            new { Text = "able" },
            new { Text = "big" },
            new { Text = "black" },
            new { Text = "certain" },
            new { Text = "clear" },
            new { Text = "different" },
            new { Text = "easy" },
            new { Text = "economic" },
            new { Text = "federal" },
            new { Text = "free" },
            new { Text = "full" },
            new { Text = "great" },
            new { Text = "hard" },
            new { Text = "high" },
            new { Text = "human" },
            new { Text = "important" },
            new { Text = "international" },
            new { Text = "large" },
            new { Text = "late" },
            new { Text = "local" },
            new { Text = "long" },
            new { Text = "low" },
            new { Text = "major" },
            new { Text = "military" },
            new { Text = "national" },
            new { Text = "new" },
            new { Text = "old" },
            new { Text = "other" },
            new { Text = "political" },
            new { Text = "possible" },
            new { Text = "public" },
            new { Text = "real" },
            new { Text = "recent" },
            new { Text = "right" },
            new { Text = "small" },
            new { Text = "social" },
            new { Text = "special" },
            new { Text = "strong" },
            new { Text = "true" },
            new { Text = "white" },
            new { Text = "whole" },
            new { Text = "young" },
        ];

        return adjectives;
    }

    private static object[] GetWords()
    {
        object[] words =
        [
            #region Quest 1

            new { Number = 1, Text = "You", Type = WordType.Pronoun, ObjectiveId = 1 },
            new { Number = 2, Text = "will not", Type = WordType.ModalVerb, ObjectiveId = 1 },
            new { Number = 3, Text = "see.", Type = WordType.Verb, ObjectiveId = 1 },

            new { Number = 1, Text = "Will", Type = WordType.ModalVerb, ObjectiveId = 2 },
            new { Number = 2, Text = "we", Type = WordType.Pronoun, ObjectiveId = 2 },
            new { Number = 3, Text = "show?", Type = WordType.Verb, ObjectiveId = 2 },

            new { Number = 1, Text = "She", Type = WordType.Pronoun, ObjectiveId = 3 },
            new { Number = 2, Text = "worked?", Type = WordType.Verb, ObjectiveId = 3 },

            new { Number = 1, Text = "You", Type = WordType.Pronoun, ObjectiveId = 4 },
            new { Number = 2, Text = "didn't", Type = WordType.PrimaryVerb, ObjectiveId = 4 },
            new { Number = 3, Text = "think.", Type = WordType.Verb, ObjectiveId = 4 },

            new { Number = 1, Text = "Will", Type = WordType.ModalVerb, ObjectiveId = 5 },
            new { Number = 2, Text = "I", Type = WordType.Pronoun, ObjectiveId = 5 },
            new { Number = 3, Text = "look?", Type = WordType.Verb, ObjectiveId = 5 },

            #endregion

            #region Quest 2

            new { Number = 1, Text = "We", Type = WordType.Pronoun, ObjectiveId = 6 },
            new { Number = 2, Text = "didn't", Type = WordType.PrimaryVerb, ObjectiveId = 6 },
            new { Number = 3, Text = "leave", Type = WordType.Verb, ObjectiveId = 6 },
            new { Number = 4, Text = "him.", Type = WordType.Pronoun, ObjectiveId = 6 },

            new { Number = 1, Text = "He", Type = WordType.Pronoun, ObjectiveId = 7 },
            new { Number = 2, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 7 },
            new { Number = 3, Text = "understand", Type = WordType.Verb, ObjectiveId = 7 },
            new { Number = 4, Text = "you.", Type = WordType.Pronoun, ObjectiveId = 7 },

            new { Number = 1, Text = "You", Type = WordType.Pronoun, ObjectiveId = 8 },
            new { Number = 2, Text = "open", Type = WordType.Verb, ObjectiveId = 8 },
            new { Number = 3, Text = "her.", Type = WordType.Pronoun, ObjectiveId = 8 },

            new { Number = 1, Text = "Will", Type = WordType.PrimaryVerb, ObjectiveId = 9 },
            new { Number = 2, Text = "I", Type = WordType.Pronoun, ObjectiveId = 9 },
            new { Number = 3, Text = "break?", Type = WordType.Verb, ObjectiveId = 9 },

            new { Number = 1, Text = "Did", Type = WordType.PrimaryVerb, ObjectiveId = 10 },
            new { Number = 2, Text = "I", Type = WordType.Pronoun, ObjectiveId = 10 },
            new { Number = 3, Text = "show", Type = WordType.Verb, ObjectiveId = 10 },
            new { Number = 4, Text = "them?", Type = WordType.Pronoun, ObjectiveId = 10 },

            #endregion

            #region Quest 3

            new { Number = 1, Text = "Do", Type = WordType.PrimaryVerb, ObjectiveId = 11 },
            new { Number = 2, Text = "you", Type = WordType.Pronoun, ObjectiveId = 11 },
            new { Number = 3, Text = "want", Type = WordType.Verb, ObjectiveId = 11 },
            new { Number = 4, Text = "to", Type = WordType.Preposition, ObjectiveId = 11 },
            new { Number = 5, Text = "drink?", Type = WordType.Verb, ObjectiveId = 11 },

            new { Number = 1, Text = "Did", Type = WordType.PrimaryVerb, ObjectiveId = 12 },
            new { Number = 2, Text = "she", Type = WordType.Pronoun, ObjectiveId = 12 },
            new { Number = 3, Text = "want", Type = WordType.Verb, ObjectiveId = 12 },
            new { Number = 4, Text = "to", Type = WordType.Preposition, ObjectiveId = 12 },
            new { Number = 5, Text = "forget?", Type = WordType.Verb, ObjectiveId = 12 },

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 13 },
            new { Number = 2, Text = "was", Type = WordType.PrimaryVerb, ObjectiveId = 13 },
            new { Number = 3, Text = "at", Type = WordType.Preposition, ObjectiveId = 13 },
            new { Number = 4, Text = "the", Type = WordType.Determiner, ObjectiveId = 13 },
            new { Number = 5, Text = "museum.", Type = WordType.Noun, ObjectiveId = 13 },

            new { Number = 1, Text = "She", Type = WordType.Pronoun, ObjectiveId = 14 },
            new { Number = 2, Text = "didn't", Type = WordType.PrimaryVerb, ObjectiveId = 14 },
            new { Number = 3, Text = "like", Type = WordType.Verb, ObjectiveId = 14 },
            new { Number = 4, Text = "to", Type = WordType.Preposition, ObjectiveId = 14 },
            new { Number = 5, Text = "show.", Type = WordType.Verb, ObjectiveId = 14 },

            new { Number = 1, Text = "Is", Type = WordType.PrimaryVerb, ObjectiveId = 15 },
            new { Number = 2, Text = "she", Type = WordType.Pronoun, ObjectiveId = 15 },
            new { Number = 3, Text = "in", Type = WordType.Preposition, ObjectiveId = 15 },
            new { Number = 4, Text = "the", Type = WordType.Determiner, ObjectiveId = 15 },
            new { Number = 5, Text = "elevator?", Type = WordType.Noun, ObjectiveId = 15 },

            #endregion

            #region Quest 4

            new { Number = 1, Text = "Will", Type = WordType.PrimaryVerb, ObjectiveId = 16 },
            new { Number = 2, Text = "he", Type = WordType.Pronoun, ObjectiveId = 16 },
            new { Number = 3, Text = "be", Type = WordType.PrimaryVerb, ObjectiveId = 16 },
            new { Number = 4, Text = "their", Type = WordType.Pronoun, ObjectiveId = 16 },
            new { Number = 5, Text = "actor?", Type = WordType.Noun, ObjectiveId = 16 },

            new { Number = 1, Text = "We", Type = WordType.Pronoun, ObjectiveId = 17 },
            new { Number = 2, Text = "are not", Type = WordType.PrimaryVerb, ObjectiveId = 17 },
            new { Number = 3, Text = "their", Type = WordType.Pronoun, ObjectiveId = 17 },
            new { Number = 4, Text = "accountants.", Type = WordType.Noun, ObjectiveId = 17 },

            new { Number = 1, Text = "We", Type = WordType.Pronoun, ObjectiveId = 18 },
            new { Number = 2, Text = "were", Type = WordType.PrimaryVerb, ObjectiveId = 18 },
            new { Number = 3, Text = "his", Type = WordType.Pronoun, ObjectiveId = 18 },
            new { Number = 4, Text = "historians.", Type = WordType.Noun, ObjectiveId = 18 },

            new { Number = 1, Text = "He", Type = WordType.Pronoun, ObjectiveId = 19 },
            new { Number = 2, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 19 },
            new { Number = 3, Text = "be", Type = WordType.PrimaryVerb, ObjectiveId = 19 },
            new { Number = 4, Text = "her", Type = WordType.Pronoun, ObjectiveId = 19 },
            new { Number = 5, Text = "writer.", Type = WordType.Noun, ObjectiveId = 19 },

            new { Number = 1, Text = "We", Type = WordType.Pronoun, ObjectiveId = 20 },
            new { Number = 2, Text = "will not", Type = WordType.PrimaryVerb, ObjectiveId = 20 },
            new { Number = 3, Text = "be", Type = WordType.PrimaryVerb, ObjectiveId = 20 },
            new { Number = 4, Text = "their", Type = WordType.Pronoun, ObjectiveId = 20 },
            new { Number = 5, Text = "guides.", Type = WordType.Noun, ObjectiveId = 20 },

            #endregion

            #region Quest 5

            new { Number = 1, Text = "Do", Type = WordType.PrimaryVerb, ObjectiveId = 21 },
            new { Number = 2, Text = "they", Type = WordType.Pronoun, ObjectiveId = 21 },
            new { Number = 3, Text = "study", Type = WordType.Verb, ObjectiveId = 21 },
            new { Number = 4, Text = "to", Type = WordType.Preposition, ObjectiveId = 21 },
            new { Number = 5, Text = "be", Type = WordType.PrimaryVerb, ObjectiveId = 21 },
            new { Number = 6, Text = "guides?", Type = WordType.Noun, ObjectiveId = 21 },

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 22 },
            new { Number = 2, Text = "will not", Type = WordType.PrimaryVerb, ObjectiveId = 22 },
            new { Number = 3, Text = "be", Type = WordType.PrimaryVerb, ObjectiveId = 22 },
            new { Number = 4, Text = "a", Type = WordType.Determiner, ObjectiveId = 22 },
            new { Number = 5, Text = "manager.", Type = WordType.Noun, ObjectiveId = 22 },

            new { Number = 1, Text = "We", Type = WordType.Pronoun, ObjectiveId = 23 },
            new { Number = 2, Text = "studied", Type = WordType.Verb, ObjectiveId = 23 },
            new { Number = 3, Text = "to", Type = WordType.Preposition, ObjectiveId = 23 },
            new { Number = 4, Text = "be", Type = WordType.PrimaryVerb, ObjectiveId = 23 },
            new { Number = 5, Text = "managers.", Type = WordType.Noun, ObjectiveId = 23 },

            new { Number = 1, Text = "Did", Type = WordType.PrimaryVerb, ObjectiveId = 24 },
            new { Number = 2, Text = "they", Type = WordType.Pronoun, ObjectiveId = 24 },
            new { Number = 3, Text = "work", Type = WordType.Verb, ObjectiveId = 24 },
            new { Number = 4, Text = "in", Type = WordType.Preposition, ObjectiveId = 24 },
            new { Number = 5, Text = "a", Type = WordType.Determiner, ObjectiveId = 24 },
            new { Number = 6, Text = "company", Type = WordType.Noun, ObjectiveId = 24 },
            new { Number = 7, Text = "as", Type = WordType.Preposition, ObjectiveId = 24 },
            new { Number = 8, Text = "designers?", Type = WordType.Noun, ObjectiveId = 24 },

            new { Number = 1, Text = "You", Type = WordType.Pronoun, ObjectiveId = 25 },
            new { Number = 2, Text = "are", Type = WordType.PrimaryVerb, ObjectiveId = 25 },
            new { Number = 3, Text = "a", Type = WordType.Determiner, ObjectiveId = 25 },
            new { Number = 4, Text = "historian.", Type = WordType.Noun, ObjectiveId = 25 },

            #endregion

            #region Quest 6

            new { Number = 1, Text = "This", Type = WordType.Pronoun, ObjectiveId = 26 },
            new { Number = 2, Text = "pen", Type = WordType.Noun, ObjectiveId = 26 },
            new { Number = 3, Text = "is not", Type = WordType.PrimaryVerb, ObjectiveId = 26 },
            new { Number = 4, Text = "bigger", Type = WordType.ComparisonAdjective, ObjectiveId = 26 },
            new { Number = 5, Text = "than", Type = WordType.Preposition, ObjectiveId = 26 },
            new { Number = 6, Text = "that", Type = WordType.Pronoun, ObjectiveId = 26 },
            new { Number = 7, Text = "one.", Type = WordType.Noun, ObjectiveId = 26 },

            new { Number = 1, Text = "This", Type = WordType.Pronoun, ObjectiveId = 27 },
            new { Number = 2, Text = "tv", Type = WordType.Noun, ObjectiveId = 27 },
            new { Number = 3, Text = "is not", Type = WordType.PrimaryVerb, ObjectiveId = 27 },
            new { Number = 4, Text = "expensive.", Type = WordType.ComparisonAdjective, ObjectiveId = 27 },

            new { Number = 1, Text = "Is", Type = WordType.PrimaryVerb, ObjectiveId = 28 },
            new { Number = 2, Text = "this", Type = WordType.Pronoun, ObjectiveId = 28 },
            new { Number = 3, Text = "phone", Type = WordType.Noun, ObjectiveId = 28 },
            new { Number = 4, Text = "cheaper", Type = WordType.ComparisonAdjective, ObjectiveId = 28 },
            new { Number = 5, Text = "than", Type = WordType.Preposition, ObjectiveId = 28 },
            new { Number = 6, Text = "that", Type = WordType.Pronoun, ObjectiveId = 28 },
            new { Number = 7, Text = "one?", Type = WordType.Noun, ObjectiveId = 28 },

            new { Number = 1, Text = "This", Type = WordType.Pronoun, ObjectiveId = 29 },
            new { Number = 2, Text = "pen", Type = WordType.Noun, ObjectiveId = 29 },
            new { Number = 3, Text = "is", Type = WordType.PrimaryVerb, ObjectiveId = 29 },
            new { Number = 4, Text = "longer", Type = WordType.ComparisonAdjective, ObjectiveId = 29 },
            new { Number = 5, Text = "than", Type = WordType.Preposition, ObjectiveId = 29 },
            new { Number = 6, Text = "that", Type = WordType.Pronoun, ObjectiveId = 29 },
            new { Number = 7, Text = "one.", Type = WordType.Noun, ObjectiveId = 29 },

            new { Number = 1, Text = "Are", Type = WordType.PrimaryVerb, ObjectiveId = 30 },
            new { Number = 2, Text = "these", Type = WordType.Pronoun, ObjectiveId = 30 },
            new { Number = 3, Text = "oranges", Type = WordType.Noun, ObjectiveId = 30 },
            new { Number = 4, Text = "the", Type = WordType.Determiner, ObjectiveId = 30 },
            new { Number = 5, Text = "least?", Type = WordType.ComparisonAdjective, ObjectiveId = 30 },

            #endregion

            #region Quest 7

            new { Number = 1, Text = "We", Type = WordType.Pronoun, ObjectiveId = 31 },
            new { Number = 2, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 31 },
            new { Number = 3, Text = "read", Type = WordType.Verb, ObjectiveId = 31 },
            new { Number = 4, Text = "somebody.", Type = WordType.Compound, ObjectiveId = 31 },

            new { Number = 1, Text = "Do", Type = WordType.PrimaryVerb, ObjectiveId = 32 },
            new { Number = 2, Text = "they", Type = WordType.Pronoun, ObjectiveId = 32 },
            new { Number = 3, Text = "feel", Type = WordType.Verb, ObjectiveId = 32 },
            new { Number = 4, Text = "everywhere?", Type = WordType.Compound, ObjectiveId = 32 },

            new { Number = 1, Text = "Do", Type = WordType.PrimaryVerb, ObjectiveId = 33 },
            new { Number = 2, Text = "you", Type = WordType.Pronoun, ObjectiveId = 33 },
            new { Number = 3, Text = "meet", Type = WordType.Verb, ObjectiveId = 33 },
            new { Number = 4, Text = "everywhere?", Type = WordType.Compound, ObjectiveId = 33 },

            new { Number = 1, Text = "Will", Type = WordType.PrimaryVerb, ObjectiveId = 34 },
            new { Number = 2, Text = "he", Type = WordType.Pronoun, ObjectiveId = 34 },
            new { Number = 3, Text = "know", Type = WordType.Verb, ObjectiveId = 34 },
            new { Number = 4, Text = "everybody?", Type = WordType.Compound, ObjectiveId = 34 },

            new { Number = 1, Text = "He", Type = WordType.Pronoun, ObjectiveId = 35 },
            new { Number = 2, Text = "never", Type = WordType.Adverb, ObjectiveId = 35 },
            new { Number = 3, Text = "sees.", Type = WordType.Verb, ObjectiveId = 35 },

            #endregion

            #region Quest 8

            new { Number = 1, Text = "She", Type = WordType.Pronoun, ObjectiveId = 36 },
            new { Number = 2, Text = "didn't", Type = WordType.PrimaryVerb, ObjectiveId = 36 },
            new { Number = 3, Text = "turn", Type = WordType.Verb, ObjectiveId = 36 },
            new { Number = 4, Text = "us", Type = WordType.Pronoun, ObjectiveId = 36 },
            new { Number = 5, Text = "six", Type = WordType.LetterNumber, ObjectiveId = 36 },
            new { Number = 6, Text = "months", Type = WordType.Noun, ObjectiveId = 36 },
            new { Number = 7, Text = "ago.", Type = WordType.Preposition, ObjectiveId = 36 },

            new { Number = 1, Text = "Will", Type = WordType.PrimaryVerb, ObjectiveId = 37 },
            new { Number = 2, Text = "he", Type = WordType.Pronoun, ObjectiveId = 37 },
            new { Number = 3, Text = "grow", Type = WordType.Verb, ObjectiveId = 37 },
            new { Number = 4, Text = "in", Type = WordType.Preposition, ObjectiveId = 37 },
            new { Number = 5, Text = "2 months?", Type = WordType.NumberWithNoun, ObjectiveId = 37 },

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 38 },
            new { Number = 2, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 38 },
            new { Number = 3, Text = "tell", Type = WordType.Verb, ObjectiveId = 38 },
            new { Number = 4, Text = "you", Type = WordType.Pronoun, ObjectiveId = 38 },
            new { Number = 5, Text = "in", Type = WordType.Preposition, ObjectiveId = 38 },
            new { Number = 6, Text = "6 months.", Type = WordType.NumberWithNoun, ObjectiveId = 38 },

            new { Number = 1, Text = "Will", Type = WordType.PrimaryVerb, ObjectiveId = 39 },
            new { Number = 2, Text = "you", Type = WordType.Pronoun, ObjectiveId = 39 },
            new { Number = 3, Text = "be", Type = WordType.PrimaryVerb, ObjectiveId = 39 },
            new { Number = 4, Text = "there", Type = WordType.Adverb, ObjectiveId = 39 },
            new { Number = 5, Text = "in", Type = WordType.Preposition, ObjectiveId = 39 },
            new { Number = 6, Text = "6 months?", Type = WordType.NumberWithNoun, ObjectiveId = 39 },

            new { Number = 1, Text = "Did", Type = WordType.PrimaryVerb, ObjectiveId = 40 },
            new { Number = 2, Text = "she", Type = WordType.Pronoun, ObjectiveId = 40 },
            new { Number = 3, Text = "love", Type = WordType.Verb, ObjectiveId = 40 },
            new { Number = 4, Text = "them", Type = WordType.Pronoun, ObjectiveId = 40 },
            new { Number = 5, Text = "at", Type = WordType.Preposition, ObjectiveId = 40 },
            new { Number = 6, Text = "the", Type = WordType.Determiner, ObjectiveId = 40 },
            new { Number = 7, Text = "weekend?", Type = WordType.Noun, ObjectiveId = 40 },

            #endregion

            #region Quest9

            new { Number = 1, Text = "There", Type = WordType.Adverb, ObjectiveId = 41 },
            new { Number = 2, Text = "were not", Type = WordType.PrimaryVerb, ObjectiveId = 41 },
            new { Number = 3, Text = "pens", Type = WordType.Noun, ObjectiveId = 41 },
            new { Number = 4, Text = "on", Type = WordType.Preposition, ObjectiveId = 41 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 41 },
            new { Number = 6, Text = "floor.", Type = WordType.Noun, ObjectiveId = 41 },

            new { Number = 1, Text = "There", Type = WordType.Adverb, ObjectiveId = 42 },
            new { Number = 2, Text = "are", Type = WordType.PrimaryVerb, ObjectiveId = 42 },
            new { Number = 3, Text = "knives", Type = WordType.Noun, ObjectiveId = 42 },
            new { Number = 4, Text = "under", Type = WordType.Preposition, ObjectiveId = 42 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 42 },
            new { Number = 6, Text = "table.", Type = WordType.Noun, ObjectiveId = 42 },

            new { Number = 1, Text = "There", Type = WordType.Adverb, ObjectiveId = 43 },
            new { Number = 2, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 43 },
            new { Number = 3, Text = "be", Type = WordType.PrimaryVerb, ObjectiveId = 43 },
            new { Number = 4, Text = "a", Type = WordType.Determiner, ObjectiveId = 43 },
            new { Number = 5, Text = "ball", Type = WordType.Noun, ObjectiveId = 43 },
            new { Number = 6, Text = "under", Type = WordType.Preposition, ObjectiveId = 43 },
            new { Number = 7, Text = "the", Type = WordType.Determiner, ObjectiveId = 43 },
            new { Number = 8, Text = "armchair.", Type = WordType.Noun, ObjectiveId = 43 },

            new { Number = 1, Text = "There", Type = WordType.Adverb, ObjectiveId = 44 },
            new { Number = 2, Text = "isn't", Type = WordType.PrimaryVerb, ObjectiveId = 44 },
            new { Number = 3, Text = "a", Type = WordType.Determiner, ObjectiveId = 44 },
            new { Number = 4, Text = "pen", Type = WordType.Noun, ObjectiveId = 44 },
            new { Number = 5, Text = "on", Type = WordType.Preposition, ObjectiveId = 44 },
            new { Number = 6, Text = "the", Type = WordType.Determiner, ObjectiveId = 44 },
            new { Number = 7, Text = "table.", Type = WordType.Noun, ObjectiveId = 44 },

            new { Number = 1, Text = "Was", Type = WordType.PrimaryVerb, ObjectiveId = 45 },
            new { Number = 2, Text = "there", Type = WordType.Adverb, ObjectiveId = 45 },
            new { Number = 3, Text = "a", Type = WordType.Determiner, ObjectiveId = 45 },
            new { Number = 4, Text = "glass", Type = WordType.Noun, ObjectiveId = 45 },
            new { Number = 5, Text = "under", Type = WordType.Preposition, ObjectiveId = 45 },
            new { Number = 6, Text = "the", Type = WordType.Determiner, ObjectiveId = 45 },
            new { Number = 7, Text = "armchair?", Type = WordType.Noun, ObjectiveId = 45 },

            #endregion

            #region Quest 10

            new { Number = 1, Text = "Does", Type = WordType.PrimaryVerb, ObjectiveId = 46 },
            new { Number = 2, Text = "he", Type = WordType.Pronoun, ObjectiveId = 46 },
            new { Number = 3, Text = "go", Type = WordType.Verb, ObjectiveId = 46 },
            new { Number = 4, Text = "on", Type = WordType.Preposition, ObjectiveId = 46 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 46 },
            new { Number = 6, Text = "station?", Type = WordType.Noun, ObjectiveId = 46 },

            new { Number = 1, Text = "You", Type = WordType.Pronoun, ObjectiveId = 47 },
            new { Number = 2, Text = "will not", Type = WordType.PrimaryVerb, ObjectiveId = 47 },
            new { Number = 3, Text = "out", Type = WordType.Adverb, ObjectiveId = 47 },
            new { Number = 4, Text = "of", Type = WordType.Preposition, ObjectiveId = 47 },
            new { Number = 5, Text = "from", Type = WordType.Preposition, ObjectiveId = 47 },
            new { Number = 6, Text = "the", Type = WordType.Determiner, ObjectiveId = 47 },
            new { Number = 7, Text = "garden.", Type = WordType.Noun, ObjectiveId = 47 },

            new { Number = 1, Text = "There", Type = WordType.Adverb, ObjectiveId = 48 },
            new { Number = 2, Text = "won't", Type = WordType.PrimaryVerb, ObjectiveId = 48 },
            new { Number = 3, Text = "be", Type = WordType.PrimaryVerb, ObjectiveId = 48 },
            new { Number = 4, Text = "cupboards", Type = WordType.Noun, ObjectiveId = 48 },
            new { Number = 5, Text = "in", Type = WordType.Preposition, ObjectiveId = 48 },
            new { Number = 6, Text = "the", Type = WordType.Determiner, ObjectiveId = 48 },
            new { Number = 7, Text = "room.", Type = WordType.Noun, ObjectiveId = 48 },

            new { Number = 1, Text = "There", Type = WordType.Adverb, ObjectiveId = 49 },
            new { Number = 2, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 49 },
            new { Number = 3, Text = "be", Type = WordType.PrimaryVerb, ObjectiveId = 49 },
            new { Number = 4, Text = "a", Type = WordType.Determiner, ObjectiveId = 49 },
            new { Number = 5, Text = "cup", Type = WordType.Noun, ObjectiveId = 49 },
            new { Number = 6, Text = "on", Type = WordType.Preposition, ObjectiveId = 49 },
            new { Number = 7, Text = "the", Type = WordType.Determiner, ObjectiveId = 49 },
            new { Number = 8, Text = "windowsill.", Type = WordType.Noun, ObjectiveId = 49 },

            new { Number = 1, Text = "Were", Type = WordType.PrimaryVerb, ObjectiveId = 50 },
            new { Number = 2, Text = "there", Type = WordType.Adverb, ObjectiveId = 50 },
            new { Number = 3, Text = "glasses", Type = WordType.Noun, ObjectiveId = 50 },
            new { Number = 4, Text = "under", Type = WordType.Preposition, ObjectiveId = 50 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 50 },
            new { Number = 6, Text = "table?", Type = WordType.Noun, ObjectiveId = 50 },

            #endregion

            #region Quest 11

            new { Number = 1, Text = "We", Type = WordType.Pronoun, ObjectiveId = 51 },
            new { Number = 2, Text = "mustn't", Type = WordType.ModalVerb, ObjectiveId = 51 },
            new { Number = 3, Text = "feel.", Type = WordType.Verb, ObjectiveId = 51 },

            new { Number = 1, Text = "Should", Type = WordType.ModalVerb, ObjectiveId = 52 },
            new { Number = 2, Text = "I", Type = WordType.Pronoun, ObjectiveId = 52 },
            new { Number = 3, Text = "stand?", Type = WordType.Verb, ObjectiveId = 52 },

            new { Number = 1, Text = "We", Type = WordType.Pronoun, ObjectiveId = 53 },
            new { Number = 2, Text = "can", Type = WordType.ModalVerb, ObjectiveId = 53 },
            new { Number = 3, Text = "close.", Type = WordType.Verb, ObjectiveId = 53 },

            new { Number = 1, Text = "We", Type = WordType.Pronoun, ObjectiveId = 54 },
            new { Number = 2, Text = "didn't", Type = WordType.PrimaryVerb, ObjectiveId = 54 },
            new { Number = 3, Text = "show.", Type = WordType.Verb, ObjectiveId = 54 },

            new { Number = 1, Text = "She", Type = WordType.Pronoun, ObjectiveId = 55 },
            new { Number = 2, Text = "must not", Type = WordType.ModalVerb, ObjectiveId = 55 },
            new { Number = 3, Text = "answer.", Type = WordType.Verb, ObjectiveId = 55 },

            #endregion

            #region Quest 12

            new { Number = 1, Text = "He", Type = WordType.Pronoun, ObjectiveId = 56 },
            new { Number = 2, Text = "was", Type = WordType.PrimaryVerb, ObjectiveId = 56 },
            new { Number = 3, Text = "reading", Type = WordType.Verb, ObjectiveId = 56 },
            new { Number = 4, Text = "the", Type = WordType.Determiner, ObjectiveId = 56 },
            new { Number = 5, Text = "letter", Type = WordType.Noun, ObjectiveId = 56 },
            new { Number = 6, Text = "from", Type = WordType.Preposition, ObjectiveId = 56 },
            new { Number = 7, Text = "four", Type = WordType.LetterNumber, ObjectiveId = 56 },
            new { Number = 8, Text = "to", Type = WordType.Preposition, ObjectiveId = 56 },
            new { Number = 9, Text = "ten", Type = WordType.LetterNumber, ObjectiveId = 56 },
            new { Number = 10, Text = "yesterday.", Type = WordType.Adverb, ObjectiveId = 56 },

            new { Number = 1, Text = "We", Type = WordType.Pronoun, ObjectiveId = 57 },
            new { Number = 2, Text = "were", Type = WordType.PrimaryVerb, ObjectiveId = 57 },
            new { Number = 3, Text = "answering", Type = WordType.Verb, ObjectiveId = 57 },
            new { Number = 4, Text = "the", Type = WordType.Determiner, ObjectiveId = 57 },
            new { Number = 5, Text = "letter", Type = WordType.Noun, ObjectiveId = 57 },
            new { Number = 6, Text = "at", Type = WordType.Preposition, ObjectiveId = 57 },
            new { Number = 7, Text = "four", Type = WordType.LetterNumber, ObjectiveId = 57 },
            new { Number = 8, Text = "yesterday.", Type = WordType.Adverb, ObjectiveId = 57 },

            new { Number = 1, Text = "You", Type = WordType.Pronoun, ObjectiveId = 58 },
            new { Number = 2, Text = "are", Type = WordType.PrimaryVerb, ObjectiveId = 58 },
            new { Number = 3, Text = "feeling", Type = WordType.Verb, ObjectiveId = 58 },
            new { Number = 4, Text = "bad", Type = WordType.Adjective, ObjectiveId = 58 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 58 },
            new { Number = 6, Text = "whole", Type = WordType.Adjective, ObjectiveId = 58 },
            new { Number = 7, Text = "day", Type = WordType.Noun, ObjectiveId = 58 },
            new { Number = 8, Text = "today.", Type = WordType.Adverb, ObjectiveId = 58 },

            new { Number = 1, Text = "We", Type = WordType.Pronoun, ObjectiveId = 59 },
            new { Number = 2, Text = "were", Type = WordType.PrimaryVerb, ObjectiveId = 59 },
            new { Number = 3, Text = "studying", Type = WordType.Verb, ObjectiveId = 59 },
            new { Number = 4, Text = "French", Type = WordType.Language, ObjectiveId = 59 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 59 },
            new { Number = 6, Text = "whole", Type = WordType.Adjective, ObjectiveId = 59 },
            new { Number = 7, Text = "day", Type = WordType.Noun, ObjectiveId = 59 },
            new { Number = 8, Text = "yesterday.", Type = WordType.Preposition, ObjectiveId = 59 },

            new { Number = 1, Text = "You", Type = WordType.Pronoun, ObjectiveId = 60 },
            new { Number = 2, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 60 },
            new { Number = 3, Text = "be", Type = WordType.PrimaryVerb, ObjectiveId = 60 },
            new { Number = 4, Text = "reading", Type = WordType.Verb, ObjectiveId = 60 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 60 },
            new { Number = 6, Text = "book", Type = WordType.Noun, ObjectiveId = 60 },
            new { Number = 7, Text = "the", Type = WordType.Determiner, ObjectiveId = 60 },
            new { Number = 8, Text = "whole", Type = WordType.Adjective, ObjectiveId = 60 },
            new { Number = 9, Text = "day", Type = WordType.Noun, ObjectiveId = 60 },
            new { Number = 10, Text = "tomorrow.", Type = WordType.Adverb, ObjectiveId = 60 },

            #endregion

            #region Quest 13

            new { Number = 1, Text = "Am", Type = WordType.PrimaryVerb, ObjectiveId = 61 },
            new { Number = 2, Text = "I", Type = WordType.Pronoun, ObjectiveId = 61 },
            new { Number = 3, Text = "cold?", Type = WordType.Adjective, ObjectiveId = 61 },

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 62 },
            new { Number = 2, Text = "do not", Type = WordType.PrimaryVerb, ObjectiveId = 62 },
            new { Number = 3, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 62 },
            new { Number = 4, Text = "blue", Type = WordType.Adverb, ObjectiveId = 62 },
            new { Number = 5, Text = "eyes.", Type = WordType.Noun, ObjectiveId = 62 },

            new { Number = 1, Text = "You", Type = WordType.Pronoun, ObjectiveId = 63 },
            new { Number = 2, Text = "are", Type = WordType.PrimaryVerb, ObjectiveId = 63 },
            new { Number = 3, Text = "healthy.", Type = WordType.Adjective, ObjectiveId = 63 },

            new { Number = 1, Text = "It", Type = WordType.Pronoun, ObjectiveId = 64 },
            new { Number = 2, Text = "was", Type = WordType.PrimaryVerb, ObjectiveId = 64 },
            new { Number = 3, Text = "hot", Type = WordType.Adjective, ObjectiveId = 64 },
            new { Number = 4, Text = "yesterday.", Type = WordType.Adverb, ObjectiveId = 64 },

            new { Number = 1, Text = "She", Type = WordType.Pronoun, ObjectiveId = 65 },
            new { Number = 2, Text = "is", Type = WordType.PrimaryVerb, ObjectiveId = 65 },
            new { Number = 3, Text = "adult.", Type = WordType.Adjective, ObjectiveId = 65 },

            #endregion

            #region Quest 14

            new { Number = 1, Text = "Run", Type = WordType.Verb, ObjectiveId = 66 },
            new { Number = 2, Text = "to", Type = WordType.Preposition, ObjectiveId = 66 },
            new { Number = 3, Text = "her.", Type = WordType.Pronoun, ObjectiveId = 66 },

            new { Number = 1, Text = "Don't", Type = WordType.PrimaryVerb, ObjectiveId = 67 },
            new { Number = 2, Text = "turn", Type = WordType.Verb, ObjectiveId = 67 },
            new { Number = 3, Text = "his", Type = WordType.Pronoun, ObjectiveId = 67 },
            new { Number = 4, Text = "nightstand.", Type = WordType.Noun, ObjectiveId = 67 },

            new { Number = 1, Text = "Don't", Type = WordType.PrimaryVerb, ObjectiveId = 68 },
            new { Number = 2, Text = "take", Type = WordType.Verb, ObjectiveId = 68 },
            new { Number = 3, Text = "my", Type = WordType.Pronoun, ObjectiveId = 68 },
            new { Number = 4, Text = "tablet.", Type = WordType.Noun, ObjectiveId = 68 },

            new { Number = 1, Text = "Remember", Type = WordType.Verb, ObjectiveId = 69 },
            new { Number = 2, Text = "about", Type = WordType.Preposition, ObjectiveId = 69 },
            new { Number = 3, Text = "us.", Type = WordType.Pronoun, ObjectiveId = 69 },

            new { Number = 1, Text = "Let", Type = WordType.Verb, ObjectiveId = 70 },
            new { Number = 2, Text = "him", Type = WordType.Pronoun, ObjectiveId = 70 },
            new { Number = 3, Text = "answer.", Type = WordType.Verb, ObjectiveId = 70 },

            #endregion

            #region Quest 15

            new { Number = 1, Text = "He", Type = WordType.Pronoun, ObjectiveId = 71 },
            new { Number = 2, Text = "took", Type = WordType.Verb, ObjectiveId = 71 },
            new { Number = 3, Text = "off", Type = WordType.Preposition, ObjectiveId = 71 },
            new { Number = 4, Text = "weight.", Type = WordType.Noun, ObjectiveId = 71 },

            new { Number = 1, Text = "Your", Type = WordType.Pronoun, ObjectiveId = 72 },
            new { Number = 2, Text = "son", Type = WordType.Noun, ObjectiveId = 72 },
            new { Number = 3, Text = "asks", Type = WordType.Verb, ObjectiveId = 72 },
            new { Number = 4, Text = "to", Type = WordType.Preposition, ObjectiveId = 72 },
            new { Number = 5, Text = "turn", Type = WordType.Verb, ObjectiveId = 72 },
            new { Number = 6, Text = "on", Type = WordType.Preposition, ObjectiveId = 72 },
            new { Number = 7, Text = "the", Type = WordType.Determiner, ObjectiveId = 72 },
            new { Number = 8, Text = "light.", Type = WordType.Noun, ObjectiveId = 72 },

            new { Number = 1, Text = "He", Type = WordType.Pronoun, ObjectiveId = 73 },
            new { Number = 2, Text = "went", Type = WordType.Verb, ObjectiveId = 73 },
            new { Number = 3, Text = "down", Type = WordType.Preposition, ObjectiveId = 73 },
            new { Number = 4, Text = "with", Type = WordType.Preposition, ObjectiveId = 73 },
            new { Number = 5, Text = "pneumonia.", Type = WordType.Noun, ObjectiveId = 73 },

            new { Number = 1, Text = "He", Type = WordType.Pronoun, ObjectiveId = 74 },
            new { Number = 2, Text = "broke", Type = WordType.Verb, ObjectiveId = 74 },
            new { Number = 3, Text = "down", Type = WordType.Preposition, ObjectiveId = 74 },
            new { Number = 4, Text = "the", Type = WordType.Determiner, ObjectiveId = 74 },
            new { Number = 5, Text = "door.", Type = WordType.Noun, ObjectiveId = 74 },

            new { Number = 1, Text = "The", Type = WordType.Determiner, ObjectiveId = 75 },
            new { Number = 2, Text = "prices", Type = WordType.Noun, ObjectiveId = 75 },
            new { Number = 3, Text = "never", Type = WordType.Adverb, ObjectiveId = 75 },
            new { Number = 4, Text = "go", Type = WordType.Verb, ObjectiveId = 75 },
            new { Number = 5, Text = "down.", Type = WordType.Preposition, ObjectiveId = 75 },

            #endregion

            #region Quest 16

            new { Number = 1, Text = "He", Type = WordType.Pronoun, ObjectiveId = 76 },
            new { Number = 2, Text = "has", Type = WordType.PrimaryVerb, ObjectiveId = 76 },
            new { Number = 3, Text = "just", Type = WordType.Adverb, ObjectiveId = 76 },
            new { Number = 4, Text = "gone", Type = WordType.Verb, ObjectiveId = 76 },
            new { Number = 5, Text = "out.", Type = WordType.Adverb, ObjectiveId = 76 },

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 77 },
            new { Number = 2, Text = "think", Type = WordType.Verb, ObjectiveId = 77 },
            new { Number = 3, Text = "I", Type = WordType.Pronoun, ObjectiveId = 77 },
            new { Number = 4, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 77 },
            new { Number = 5, Text = "seen", Type = WordType.Verb, ObjectiveId = 77 },
            new { Number = 6, Text = "you", Type = WordType.Pronoun, ObjectiveId = 77 },
            new { Number = 7, Text = "somewhere.", Type = WordType.Compound, ObjectiveId = 77 },

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 78 },
            new { Number = 2, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 78 },
            new { Number = 3, Text = "done", Type = WordType.Verb, ObjectiveId = 78 },
            new { Number = 4, Text = "the", Type = WordType.Determiner, ObjectiveId = 78 },
            new { Number = 5, Text = "work.", Type = WordType.Verb, ObjectiveId = 78 },

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 79 },
            new { Number = 2, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 79 },
            new { Number = 3, Text = "already", Type = WordType.Adverb, ObjectiveId = 79 },
            new { Number = 4, Text = "written", Type = WordType.Verb, ObjectiveId = 79 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 79 },
            new { Number = 6, Text = "letter", Type = WordType.Noun, ObjectiveId = 79 },
            new { Number = 7, Text = "to", Type = WordType.Preposition, ObjectiveId = 79 },
            new { Number = 8, Text = "my", Type = WordType.Pronoun, ObjectiveId = 79 },
            new { Number = 9, Text = "friend.", Type = WordType.Noun, ObjectiveId = 79 },

            new { Number = 1, Text = "He", Type = WordType.Pronoun, ObjectiveId = 80 },
            new { Number = 2, Text = "has", Type = WordType.PrimaryVerb, ObjectiveId = 80 },
            new { Number = 3, Text = "just", Type = WordType.Adverb, ObjectiveId = 80 },
            new { Number = 4, Text = "visited", Type = WordType.Verb, ObjectiveId = 80 },
            new { Number = 5, Text = "this", Type = WordType.Pronoun, ObjectiveId = 80 },
            new { Number = 6, Text = "supermarket.", Type = WordType.Noun, ObjectiveId = 80 },

            #endregion

            #region Quest 17

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 81 },
            new { Number = 2, Text = "had not", Type = WordType.PrimaryVerb, ObjectiveId = 81 },
            new { Number = 3, Text = "finished", Type = WordType.Verb, ObjectiveId = 81 },
            new { Number = 4, Text = "the", Type = WordType.Determiner, ObjectiveId = 81 },
            new { Number = 5, Text = "project", Type = WordType.Noun, ObjectiveId = 81 },
            new { Number = 6, Text = "by", Type = WordType.Preposition, ObjectiveId = 81 },
            new { Number = 7, Text = "the", Type = WordType.Determiner, ObjectiveId = 81 },
            new { Number = 8, Text = "beginning", Type = WordType.Verb, ObjectiveId = 81 },
            new { Number = 9, Text = "of", Type = WordType.Preposition, ObjectiveId = 81 },
            new { Number = 10, Text = "that", Type = WordType.Pronoun, ObjectiveId = 81 },
            new { Number = 11, Text = "week.", Type = WordType.Noun, ObjectiveId = 81 },

            new { Number = 1, Text = "Had", Type = WordType.PrimaryVerb, ObjectiveId = 82 },
            new { Number = 2, Text = "he", Type = WordType.Pronoun, ObjectiveId = 82 },
            new { Number = 3, Text = "done", Type = WordType.Verb, ObjectiveId = 82 },
            new { Number = 4, Text = "his", Type = WordType.Pronoun, ObjectiveId = 82 },
            new { Number = 5, Text = "homework", Type = WordType.Noun, ObjectiveId = 82 },
            new { Number = 6, Text = "before", Type = WordType.Preposition, ObjectiveId = 82 },
            new { Number = 7, Text = "his", Type = WordType.PrimaryVerb, ObjectiveId = 82 },
            new { Number = 8, Text = "parents", Type = WordType.Noun, ObjectiveId = 82 },
            new { Number = 9, Text = "returned", Type = WordType.Verb, ObjectiveId = 82 },
            new { Number = 10, Text = "home?", Type = WordType.Noun, ObjectiveId = 82 },

            new { Number = 1, Text = "Had", Type = WordType.PrimaryVerb, ObjectiveId = 83 },
            new { Number = 2, Text = "he", Type = WordType.Pronoun, ObjectiveId = 83 },
            new { Number = 3, Text = "written", Type = WordType.Verb, ObjectiveId = 83 },
            new { Number = 4, Text = "a", Type = WordType.Determiner, ObjectiveId = 83 },
            new { Number = 5, Text = "letter,", Type = WordType.Noun, ObjectiveId = 83 },
            new { Number = 6, Text = "when", Type = WordType.QuestionWord, ObjectiveId = 83 },
            new { Number = 7, Text = "you", Type = WordType.Pronoun, ObjectiveId = 83 },
            new { Number = 8, Text = "came", Type = WordType.Verb, ObjectiveId = 83 },
            new { Number = 9, Text = "in?", Type = WordType.Preposition, ObjectiveId = 83 },

            new { Number = 1, Text = "You", Type = WordType.Pronoun, ObjectiveId = 84 },
            new { Number = 2, Text = "had", Type = WordType.PrimaryVerb, ObjectiveId = 84 },
            new { Number = 3, Text = "returned", Type = WordType.Verb, ObjectiveId = 84 },
            new { Number = 4, Text = "home", Type = WordType.Noun, ObjectiveId = 84 },
            new { Number = 5, Text = "before", Type = WordType.Preposition, ObjectiveId = 84 },
            new { Number = 6, Text = "the", Type = WordType.Determiner, ObjectiveId = 84 },
            new { Number = 7, Text = "rain", Type = WordType.Noun, ObjectiveId = 84 },
            new { Number = 8, Text = "began.", Type = WordType.Verb, ObjectiveId = 84 },

            new { Number = 1, Text = "When", Type = WordType.QuestionWord, ObjectiveId = 85 },
            new { Number = 2, Text = "we", Type = WordType.Pronoun, ObjectiveId = 85 },
            new { Number = 3, Text = "came", Type = WordType.Verb, ObjectiveId = 85 },
            new { Number = 4, Text = "to", Type = WordType.Preposition, ObjectiveId = 85 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 85 },
            new { Number = 6, Text = "station", Type = WordType.Noun, ObjectiveId = 85 },
            new { Number = 7, Text = "the", Type = WordType.Determiner, ObjectiveId = 85 },
            new { Number = 8, Text = "train", Type = WordType.Noun, ObjectiveId = 85 },
            new { Number = 9, Text = "had", Type = WordType.PrimaryVerb, ObjectiveId = 85 },
            new { Number = 10, Text = "already", Type = WordType.Adverb, ObjectiveId = 85 },
            new { Number = 11, Text = "gone.", Type = WordType.Verb, ObjectiveId = 85 },

            #endregion

            #region Quest 18

            new { Number = 1, Text = "The", Type = WordType.Determiner, ObjectiveId = 86 },
            new { Number = 2, Text = "pupils", Type = WordType.Noun, ObjectiveId = 86 },
            new { Number = 3, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 86 },
            new { Number = 4, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 86 },
            new { Number = 5, Text = "taken", Type = WordType.Verb, ObjectiveId = 86 },
            new { Number = 6, Text = "their", Type = WordType.Pronoun, ObjectiveId = 86 },
            new { Number = 7, Text = "seats", Type = WordType.Noun, ObjectiveId = 86 },
            new { Number = 8, Text = "before", Type = WordType.Preposition, ObjectiveId = 86 },
            new { Number = 9, Text = "the", Type = WordType.Determiner, ObjectiveId = 86 },
            new { Number = 10, Text = "quest", Type = WordType.Noun, ObjectiveId = 86 },
            new { Number = 11, Text = "starts.", Type = WordType.Verb, ObjectiveId = 86 },

            new { Number = 1, Text = "They", Type = WordType.Pronoun, ObjectiveId = 87 },
            new { Number = 2, Text = "will not", Type = WordType.PrimaryVerb, ObjectiveId = 87 },
            new { Number = 3, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 87 },
            new { Number = 4, Text = "won", Type = WordType.Verb, ObjectiveId = 87 },
            new { Number = 5, Text = "three", Type = WordType.LetterNumber, ObjectiveId = 87 },
            new { Number = 6, Text = "games", Type = WordType.Noun, ObjectiveId = 87 },
            new { Number = 7, Text = "by", Type = WordType.Preposition, ObjectiveId = 87 },
            new { Number = 8, Text = "the", Type = WordType.Determiner, ObjectiveId = 87 },
            new { Number = 9, Text = "end", Type = WordType.Noun, ObjectiveId = 87 },
            new { Number = 10, Text = "of", Type = WordType.Preposition, ObjectiveId = 87 },
            new { Number = 11, Text = "the", Type = WordType.Determiner, ObjectiveId = 87 },
            new { Number = 12, Text = "month.", Type = WordType.Noun, ObjectiveId = 87 },

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 88 },
            new { Number = 2, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 88 },
            new { Number = 3, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 88 },
            new { Number = 4, Text = "finished", Type = WordType.Verb, ObjectiveId = 88 },
            new { Number = 5, Text = "this", Type = WordType.Pronoun, ObjectiveId = 88 },
            new { Number = 6, Text = "work,", Type = WordType.Verb, ObjectiveId = 88 },
            new { Number = 7, Text = "before", Type = WordType.Preposition, ObjectiveId = 88 },
            new { Number = 8, Text = "you", Type = WordType.Pronoun, ObjectiveId = 88 },
            new { Number = 9, Text = "return.", Type = WordType.Verb, ObjectiveId = 88 },

            new { Number = 1, Text = "They", Type = WordType.Pronoun, ObjectiveId = 89 },
            new { Number = 2, Text = "will not", Type = WordType.PrimaryVerb, ObjectiveId = 89 },
            new { Number = 3, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 89 },
            new { Number = 4, Text = "shipped", Type = WordType.Verb, ObjectiveId = 89 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 89 },
            new { Number = 6, Text = "goods", Type = WordType.Noun, ObjectiveId = 89 },
            new { Number = 7, Text = "when", Type = WordType.QuestionWord, ObjectiveId = 89 },
            new { Number = 8, Text = "your", Type = WordType.Pronoun, ObjectiveId = 89 },
            new { Number = 9, Text = "telegram", Type = WordType.Noun, ObjectiveId = 89 },
            new { Number = 10, Text = "arrives.", Type = WordType.Verb, ObjectiveId = 89 },

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 90 },
            new { Number = 2, Text = "will not", Type = WordType.PrimaryVerb, ObjectiveId = 90 },
            new { Number = 3, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 90 },
            new { Number = 4, Text = "finished", Type = WordType.Verb, ObjectiveId = 90 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 90 },
            new { Number = 6, Text = "report", Type = WordType.Noun, ObjectiveId = 90 },
            new { Number = 7, Text = "by", Type = WordType.Preposition, ObjectiveId = 90 },
            new { Number = 8, Text = "tonight.", Type = WordType.Adverb, ObjectiveId = 90 },

            #endregion

            #region Quest 20

            new { Number = 1, Text = "She", Type = WordType.Pronoun, ObjectiveId = 91 },
            new { Number = 2, Text = "has", Type = WordType.PrimaryVerb, ObjectiveId = 91 },
            new { Number = 3, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 91 },
            new { Number = 4, Text = "cooking", Type = WordType.Verb, ObjectiveId = 91 },
            new { Number = 5, Text = "dinner", Type = WordType.Noun, ObjectiveId = 91 },
            new { Number = 6, Text = "for", Type = WordType.Preposition, ObjectiveId = 91 },
            new { Number = 7, Text = "three", Type = WordType.LetterNumber, ObjectiveId = 91 },
            new { Number = 8, Text = "hours.", Type = WordType.Noun, ObjectiveId = 91 },

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 92 },
            new { Number = 2, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 92 },
            new { Number = 3, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 92 },
            new { Number = 4, Text = "baking", Type = WordType.Verb, ObjectiveId = 92 },
            new { Number = 5, Text = "this", Type = WordType.Pronoun, ObjectiveId = 92 },
            new { Number = 6, Text = "cake", Type = WordType.Noun, ObjectiveId = 92 },
            new { Number = 7, Text = "since", Type = WordType.Preposition, ObjectiveId = 92 },
            new { Number = 8, Text = "morning.", Type = WordType.Noun, ObjectiveId = 92 },

            new { Number = 1, Text = "The", Type = WordType.Determiner, ObjectiveId = 93 },
            new { Number = 2, Text = "workers", Type = WordType.Noun, ObjectiveId = 93 },
            new { Number = 3, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 93 },
            new { Number = 4, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 93 },
            new { Number = 5, Text = "trying", Type = WordType.Verb, ObjectiveId = 93 },
            new { Number = 6, Text = "to", Type = WordType.Preposition, ObjectiveId = 93 },
            new { Number = 7, Text = "move", Type = WordType.Verb, ObjectiveId = 93 },
            new { Number = 8, Text = "our", Type = WordType.Pronoun, ObjectiveId = 93 },
            new { Number = 9, Text = "cupboard", Type = WordType.Noun, ObjectiveId = 93 },
            new { Number = 10, Text = "for", Type = WordType.Preposition, ObjectiveId = 93 },
            new { Number = 11, Text = "half", Type = WordType.Noun, ObjectiveId = 93 },
            new { Number = 12, Text = "an", Type = WordType.Determiner, ObjectiveId = 93 },
            new { Number = 13, Text = "hour.", Type = WordType.Noun, ObjectiveId = 93 },

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 94 },
            new { Number = 2, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 94 },
            new { Number = 3, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 94 },
            new { Number = 4, Text = "reading", Type = WordType.Verb, ObjectiveId = 94 },
            new { Number = 5, Text = "this", Type = WordType.Pronoun, ObjectiveId = 94 },
            new { Number = 6, Text = "magazine", Type = WordType.Noun, ObjectiveId = 94 },
            new { Number = 7, Text = "since", Type = WordType.Preposition, ObjectiveId = 94 },
            new { Number = 8, Text = "I", Type = WordType.Pronoun, ObjectiveId = 94 },
            new { Number = 9, Text = "bought", Type = WordType.Verb, ObjectiveId = 94 },
            new { Number = 10, Text = "it", Type = WordType.Pronoun, ObjectiveId = 94 },
            new { Number = 11, Text = "a", Type = WordType.Determiner, ObjectiveId = 94 },
            new { Number = 12, Text = "week", Type = WordType.Noun, ObjectiveId = 94 },
            new { Number = 13, Text = "ago.", Type = WordType.Preposition, ObjectiveId = 94 },

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 95 },
            new { Number = 2, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 95 },
            new { Number = 3, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 95 },
            new { Number = 4, Text = "waiting", Type = WordType.Verb, ObjectiveId = 95 },
            new { Number = 5, Text = "for", Type = WordType.Preposition, ObjectiveId = 95 },
            new { Number = 6, Text = "my", Type = WordType.Pronoun, ObjectiveId = 95 },
            new { Number = 7, Text = "mother", Type = WordType.Noun, ObjectiveId = 95 },
            new { Number = 8, Text = "for", Type = WordType.Preposition, ObjectiveId = 95 },
            new { Number = 9, Text = "a", Type = WordType.Determiner, ObjectiveId = 95 },
            new { Number = 10, Text = "long", Type = WordType.Adjective, ObjectiveId = 95 },
            new { Number = 11, Text = "time.", Type = WordType.Noun, ObjectiveId = 95 },

            #endregion

            #region Quest 21

            new { Number = 1, Text = "Since", Type = WordType.Preposition, ObjectiveId = 96 },
            new { Number = 2, Text = "then", Type = WordType.Preposition, ObjectiveId = 96 },
            new { Number = 3, Text = "the", Type = WordType.Determiner, ObjectiveId = 96 },
            new { Number = 4, Text = "index", Type = WordType.Noun, ObjectiveId = 96 },
            new { Number = 5, Text = "had", Type = WordType.PrimaryVerb, ObjectiveId = 96 },
            new { Number = 6, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 96 },
            new { Number = 7, Text = "rising", Type = WordType.Verb, ObjectiveId = 96 },
            new { Number = 8, Text = "fast.", Type = WordType.Adjective, ObjectiveId = 96 },

            new { Number = 1, Text = "His", Type = WordType.Pronoun, ObjectiveId = 97 },
            new { Number = 2, Text = "hands", Type = WordType.Noun, ObjectiveId = 97 },
            new { Number = 3, Text = "were", Type = WordType.PrimaryVerb, ObjectiveId = 97 },
            new { Number = 4, Text = "dirty", Type = WordType.Adjective, ObjectiveId = 97 },
            new { Number = 5, Text = "he", Type = WordType.Pronoun, ObjectiveId = 97 },
            new { Number = 6, Text = "had", Type = WordType.PrimaryVerb, ObjectiveId = 97 },
            new { Number = 7, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 97 },
            new { Number = 8, Text = "digging.", Type = WordType.Verb, ObjectiveId = 97 },

            new { Number = 1, Text = "They", Type = WordType.Pronoun, ObjectiveId = 98 },
            new { Number = 2, Text = "had", Type = WordType.PrimaryVerb, ObjectiveId = 98 },
            new { Number = 3, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 98 },
            new { Number = 4, Text = "talking", Type = WordType.Verb, ObjectiveId = 98 },
            new { Number = 5, Text = "for", Type = WordType.Preposition, ObjectiveId = 98 },
            new { Number = 6, Text = "over", Type = WordType.Preposition, ObjectiveId = 98 },
            new { Number = 7, Text = "an", Type = WordType.Determiner, ObjectiveId = 98 },
            new { Number = 8, Text = "hour", Type = WordType.Noun, ObjectiveId = 98 },
            new { Number = 9, Text = "before", Type = WordType.Preposition, ObjectiveId = 98 },
            new { Number = 10, Text = "he", Type = WordType.Pronoun, ObjectiveId = 98 },
            new { Number = 11, Text = "arrived.", Type = WordType.Verb, ObjectiveId = 98 },

            new { Number = 1, Text = "It", Type = WordType.Pronoun, ObjectiveId = 99 },
            new { Number = 2, Text = "was", Type = WordType.PrimaryVerb, ObjectiveId = 99 },
            new { Number = 3, Text = "one", Type = WordType.Noun, ObjectiveId = 99 },
            new { Number = 4, Text = "o'clock", Type = WordType.Adverb, ObjectiveId = 99 },
            new { Number = 5, Text = "and", Type = WordType.Preposition, ObjectiveId = 99 },
            new { Number = 6, Text = "the", Type = WordType.Determiner, ObjectiveId = 99 },
            new { Number = 7, Text = "dog", Type = WordType.Noun, ObjectiveId = 99 },
            new { Number = 8, Text = "from", Type = WordType.Preposition, ObjectiveId = 99 },
            new { Number = 9, Text = "next", Type = WordType.Adjective, ObjectiveId = 99 },
            new { Number = 10, Text = "door", Type = WordType.Noun, ObjectiveId = 99 },
            new { Number = 11, Text = "had", Type = WordType.PrimaryVerb, ObjectiveId = 99 },
            new { Number = 12, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 99 },
            new { Number = 13, Text = "barking", Type = WordType.Verb, ObjectiveId = 99 },
            new { Number = 14, Text = "for", Type = WordType.Preposition, ObjectiveId = 99 },
            new { Number = 15, Text = "two", Type = WordType.LetterNumber, ObjectiveId = 99 },
            new { Number = 16, Text = "hours.", Type = WordType.Noun, ObjectiveId = 99 },

            new { Number = 1, Text = "My", Type = WordType.Pronoun, ObjectiveId = 100 },
            new { Number = 2, Text = "dog", Type = WordType.Noun, ObjectiveId = 100 },
            new { Number = 3, Text = "had", Type = WordType.PrimaryVerb, ObjectiveId = 100 },
            new { Number = 4, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 100 },
            new { Number = 5, Text = "playing", Type = WordType.Verb, ObjectiveId = 100 },
            new { Number = 6, Text = "for", Type = WordType.Preposition, ObjectiveId = 100 },
            new { Number = 7, Text = "half", Type = WordType.Noun, ObjectiveId = 100 },
            new { Number = 8, Text = "an", Type = WordType.Determiner, ObjectiveId = 100 },
            new { Number = 9, Text = "hour", Type = WordType.Noun, ObjectiveId = 100 },
            new { Number = 10, Text = "before", Type = WordType.Preposition, ObjectiveId = 100 },
            new { Number = 11, Text = "we", Type = WordType.Pronoun, ObjectiveId = 100 },
            new { Number = 12, Text = "went", Type = WordType.Verb, ObjectiveId = 100 },
            new { Number = 13, Text = "for", Type = WordType.Preposition, ObjectiveId = 100 },
            new { Number = 14, Text = "a", Type = WordType.Determiner, ObjectiveId = 100 },
            new { Number = 15, Text = "walk.", Type = WordType.Verb, ObjectiveId = 100 },

            #endregion

            #region Quest 22

            new { Number = 1, Text = "They", Type = WordType.Pronoun, ObjectiveId = 101 },
            new { Number = 2, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 101 },
            new { Number = 3, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 101 },
            new { Number = 4, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 101 },
            new { Number = 5, Text = "talking", Type = WordType.Verb, ObjectiveId = 101 },
            new { Number = 6, Text = "for", Type = WordType.Preposition, ObjectiveId = 101 },
            new { Number = 7, Text = "over", Type = WordType.Preposition, ObjectiveId = 101 },
            new { Number = 8, Text = "an", Type = WordType.Determiner, ObjectiveId = 101 },
            new { Number = 9, Text = "hour,", Type = WordType.Noun, ObjectiveId = 101 },
            new { Number = 10, Text = "by", Type = WordType.Preposition, ObjectiveId = 101 },
            new { Number = 11, Text = "the", Type = WordType.Determiner, ObjectiveId = 101 },
            new { Number = 12, Text = "time,", Type = WordType.Noun, ObjectiveId = 101 },
            new { Number = 13, Text = "he", Type = WordType.Pronoun, ObjectiveId = 101 },
            new { Number = 14, Text = "arrives.", Type = WordType.Verb, ObjectiveId = 101 },

            new { Number = 1, Text = "By", Type = WordType.Preposition, ObjectiveId = 102 },
            new { Number = 2, Text = "the", Type = WordType.Determiner, ObjectiveId = 102 },
            new { Number = 3, Text = "first", Type = WordType.Adverb, ObjectiveId = 102 },
            new { Number = 4, Text = "of", Type = WordType.Preposition, ObjectiveId = 102 },
            new { Number = 5, Text = "June", Type = WordType.Noun, ObjectiveId = 102 },
            new { Number = 6, Text = "he", Type = WordType.Pronoun, ObjectiveId = 102 },
            new { Number = 7, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 102 },
            new { Number = 8, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 102 },
            new { Number = 9, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 102 },
            new { Number = 10, Text = "working", Type = WordType.Verb, ObjectiveId = 102 },
            new { Number = 11, Text = "at", Type = WordType.Preposition, ObjectiveId = 102 },
            new { Number = 12, Text = "this", Type = WordType.Pronoun, ObjectiveId = 102 },
            new { Number = 13, Text = "plant", Type = WordType.Noun, ObjectiveId = 102 },
            new { Number = 14, Text = "for", Type = WordType.Preposition, ObjectiveId = 102 },
            new { Number = 15, Text = "twenty", Type = WordType.LetterNumber, ObjectiveId = 102 },
            new { Number = 16, Text = "years.", Type = WordType.Noun, ObjectiveId = 102 },

            new { Number = 1, Text = "Next", Type = WordType.Adjective, ObjectiveId = 103 },
            new { Number = 2, Text = "Christmas", Type = WordType.Noun, ObjectiveId = 103 },
            new { Number = 3, Text = "I", Type = WordType.Pronoun, ObjectiveId = 103 },
            new { Number = 4, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 103 },
            new { Number = 5, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 103 },
            new { Number = 6, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 103 },
            new { Number = 7, Text = "teaching", Type = WordType.Verb, ObjectiveId = 103 },
            new { Number = 8, Text = "for", Type = WordType.Preposition, ObjectiveId = 103 },
            new { Number = 9, Text = "ten", Type = WordType.LetterNumber, ObjectiveId = 103 },
            new { Number = 10, Text = "years.", Type = WordType.Noun, ObjectiveId = 103 },

            new { Number = 1, Text = "Will", Type = WordType.PrimaryVerb, ObjectiveId = 104 },
            new { Number = 2, Text = "you", Type = WordType.Pronoun, ObjectiveId = 104 },
            new { Number = 3, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 104 },
            new { Number = 4, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 104 },
            new { Number = 5, Text = "waiting", Type = WordType.Verb, ObjectiveId = 104 },
            new { Number = 6, Text = "for", Type = WordType.Preposition, ObjectiveId = 104 },
            new { Number = 7, Text = "more", Type = WordType.Adverb, ObjectiveId = 104 },
            new { Number = 8, Text = "than", Type = WordType.Preposition, ObjectiveId = 104 },
            new { Number = 9, Text = "two", Type = WordType.LetterNumber, ObjectiveId = 104 },
            new { Number = 10, Text = "hours,", Type = WordType.Noun, ObjectiveId = 104 },
            new { Number = 11, Text = "when", Type = WordType.QuestionWord, ObjectiveId = 104 },
            new { Number = 12, Text = "her", Type = WordType.Pronoun, ObjectiveId = 104 },
            new { Number = 13, Text = "plane", Type = WordType.Noun, ObjectiveId = 104 },
            new { Number = 14, Text = "finally", Type = WordType.Adverb, ObjectiveId = 104 },
            new { Number = 15, Text = "arrives?", Type = WordType.Verb, ObjectiveId = 104 },

            new { Number = 1, Text = "Next", Type = WordType.Adjective, ObjectiveId = 105 },
            new { Number = 2, Text = "month", Type = WordType.Noun, ObjectiveId = 105 },
            new { Number = 3, Text = "we", Type = WordType.Pronoun, ObjectiveId = 105 },
            new { Number = 4, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 105 },
            new { Number = 5, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 105 },
            new { Number = 6, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 105 },
            new { Number = 7, Text = "living", Type = WordType.Verb, ObjectiveId = 105 },
            new { Number = 8, Text = "together", Type = WordType.Preposition, ObjectiveId = 105 },
            new { Number = 9, Text = "for", Type = WordType.Preposition, ObjectiveId = 105 },
            new { Number = 10, Text = "fifteen", Type = WordType.LetterNumber, ObjectiveId = 105 },
            new { Number = 11, Text = "years.", Type = WordType.Noun, ObjectiveId = 105 },

            #endregion

            #region Quest 24

            new { Number = 1, Text = "They", Type = WordType.Pronoun, ObjectiveId = 106 },
            new { Number = 2, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 106 },
            new { Number = 3, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 106 },
            new { Number = 4, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 106 },
            new { Number = 5, Text = "talking", Type = WordType.Verb, ObjectiveId = 106 },
            new { Number = 6, Text = "for", Type = WordType.Preposition, ObjectiveId = 106 },
            new { Number = 7, Text = "over", Type = WordType.Preposition, ObjectiveId = 106 },
            new { Number = 8, Text = "an", Type = WordType.Determiner, ObjectiveId = 106 },
            new { Number = 9, Text = "hour,", Type = WordType.Noun, ObjectiveId = 106 },
            new { Number = 10, Text = "by", Type = WordType.Preposition, ObjectiveId = 106 },
            new { Number = 11, Text = "the", Type = WordType.Determiner, ObjectiveId = 106 },
            new { Number = 12, Text = "time,", Type = WordType.Noun, ObjectiveId = 106 },
            new { Number = 13, Text = "he", Type = WordType.Pronoun, ObjectiveId = 106 },
            new { Number = 14, Text = "arrives.", Type = WordType.Verb, ObjectiveId = 106 },

            new { Number = 1, Text = "By", Type = WordType.Preposition, ObjectiveId = 107 },
            new { Number = 2, Text = "the", Type = WordType.Determiner, ObjectiveId = 107 },
            new { Number = 3, Text = "first", Type = WordType.Adverb, ObjectiveId = 107 },
            new { Number = 4, Text = "of", Type = WordType.Preposition, ObjectiveId = 107 },
            new { Number = 5, Text = "June", Type = WordType.Noun, ObjectiveId = 107 },
            new { Number = 6, Text = "he", Type = WordType.Pronoun, ObjectiveId = 107 },
            new { Number = 7, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 107 },
            new { Number = 8, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 107 },
            new { Number = 9, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 107 },
            new { Number = 10, Text = "working", Type = WordType.Verb, ObjectiveId = 107 },
            new { Number = 11, Text = "at", Type = WordType.Preposition, ObjectiveId = 107 },
            new { Number = 12, Text = "this", Type = WordType.Pronoun, ObjectiveId = 107 },
            new { Number = 13, Text = "plant", Type = WordType.Noun, ObjectiveId = 107 },
            new { Number = 14, Text = "for", Type = WordType.Preposition, ObjectiveId = 107 },
            new { Number = 15, Text = "twenty", Type = WordType.LetterNumber, ObjectiveId = 107 },
            new { Number = 16, Text = "years.", Type = WordType.Noun, ObjectiveId = 107 },

            new { Number = 1, Text = "Next", Type = WordType.Adjective, ObjectiveId = 108 },
            new { Number = 2, Text = "Christmas", Type = WordType.Noun, ObjectiveId = 108 },
            new { Number = 3, Text = "I", Type = WordType.Pronoun, ObjectiveId = 108 },
            new { Number = 4, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 108 },
            new { Number = 5, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 108 },
            new { Number = 6, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 108 },
            new { Number = 7, Text = "teaching", Type = WordType.Verb, ObjectiveId = 108 },
            new { Number = 8, Text = "for", Type = WordType.Preposition, ObjectiveId = 108 },
            new { Number = 9, Text = "ten", Type = WordType.LetterNumber, ObjectiveId = 108 },
            new { Number = 10, Text = "years.", Type = WordType.Noun, ObjectiveId = 108 },

            new { Number = 1, Text = "Will", Type = WordType.PrimaryVerb, ObjectiveId = 109 },
            new { Number = 2, Text = "you", Type = WordType.Pronoun, ObjectiveId = 109 },
            new { Number = 3, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 109 },
            new { Number = 4, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 109 },
            new { Number = 5, Text = "waiting", Type = WordType.Verb, ObjectiveId = 109 },
            new { Number = 6, Text = "for", Type = WordType.Preposition, ObjectiveId = 109 },
            new { Number = 7, Text = "more", Type = WordType.Adverb, ObjectiveId = 109 },
            new { Number = 8, Text = "than", Type = WordType.Preposition, ObjectiveId = 109 },
            new { Number = 9, Text = "two", Type = WordType.LetterNumber, ObjectiveId = 109 },
            new { Number = 10, Text = "hours,", Type = WordType.Noun, ObjectiveId = 109 },
            new { Number = 11, Text = "when", Type = WordType.QuestionWord, ObjectiveId = 109 },
            new { Number = 12, Text = "her", Type = WordType.Pronoun, ObjectiveId = 109 },
            new { Number = 13, Text = "plane", Type = WordType.Noun, ObjectiveId = 109 },
            new { Number = 14, Text = "finally", Type = WordType.Adverb, ObjectiveId = 109 },
            new { Number = 15, Text = "arrives?", Type = WordType.Verb, ObjectiveId = 109 },

            new { Number = 1, Text = "Next", Type = WordType.Adjective, ObjectiveId = 110 },
            new { Number = 2, Text = "month", Type = WordType.Noun, ObjectiveId = 110 },
            new { Number = 3, Text = "we", Type = WordType.Pronoun, ObjectiveId = 110 },
            new { Number = 4, Text = "will", Type = WordType.PrimaryVerb, ObjectiveId = 110 },
            new { Number = 5, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 110 },
            new { Number = 6, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 110 },
            new { Number = 7, Text = "living", Type = WordType.Verb, ObjectiveId = 110 },
            new { Number = 8, Text = "together", Type = WordType.Preposition, ObjectiveId = 110 },
            new { Number = 9, Text = "for", Type = WordType.Preposition, ObjectiveId = 110 },
            new { Number = 10, Text = "fifteen", Type = WordType.LetterNumber, ObjectiveId = 110 },
            new { Number = 11, Text = "years.", Type = WordType.Noun, ObjectiveId = 110 },

            #endregion

            #region Quest 25

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 111 },
            new { Number = 2, Text = "was", Type = WordType.PrimaryVerb, ObjectiveId = 111 },
            new { Number = 3, Text = "disappointed", Type = WordType.Verb, ObjectiveId = 111 },
            new { Number = 4, Text = "to", Type = WordType.Preposition, ObjectiveId = 111 },
            new { Number = 5, Text = "hear", Type = WordType.Verb, ObjectiveId = 111 },
            new { Number = 6, Text = "that", Type = WordType.Pronoun, ObjectiveId = 111 },
            new { Number = 7, Text = "more", Type = WordType.Adverb, ObjectiveId = 111 },
            new { Number = 8, Text = "and", Type = WordType.Preposition, ObjectiveId = 111 },
            new { Number = 9, Text = "more", Type = WordType.Adverb, ObjectiveId = 111 },
            new { Number = 10, Text = "people", Type = WordType.Noun, ObjectiveId = 111 },
            new { Number = 11, Text = "lose", Type = WordType.Verb, ObjectiveId = 111 },
            new { Number = 12, Text = "their", Type = WordType.Pronoun, ObjectiveId = 111 },
            new { Number = 13, Text = "jobs.", Type = WordType.Noun, ObjectiveId = 111 },

            new { Number = 1, Text = "Exhausted,", Type = WordType.Verb, ObjectiveId = 112 },
            new { Number = 2, Text = "he", Type = WordType.Pronoun, ObjectiveId = 112 },
            new { Number = 3, Text = "fell", Type = WordType.Verb, ObjectiveId = 112 },
            new { Number = 4, Text = "asleep.", Type = WordType.Adjective, ObjectiveId = 112 },

            new { Number = 1, Text = "The", Type = WordType.Determiner, ObjectiveId = 113 },
            new { Number = 2, Text = "appliance", Type = WordType.Noun, ObjectiveId = 113 },
            new { Number = 3, Text = "bought", Type = WordType.Verb, ObjectiveId = 113 },
            new { Number = 4, Text = "from", Type = WordType.Preposition, ObjectiveId = 113 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 113 },
            new { Number = 6, Text = "specialized", Type = WordType.Adjective, ObjectiveId = 113 },
            new { Number = 7, Text = "shop", Type = WordType.Noun, ObjectiveId = 113 },
            new { Number = 8, Text = "has", Type = WordType.PrimaryVerb, ObjectiveId = 113 },
            new { Number = 9, Text = "a", Type = WordType.Determiner, ObjectiveId = 113 },
            new { Number = 10, Text = "guarantee.", Type = WordType.Noun, ObjectiveId = 113 },

            new { Number = 1, Text = "They", Type = WordType.Pronoun, ObjectiveId = 114 },
            new { Number = 2, Text = "could not", Type = WordType.ModalVerb, ObjectiveId = 114 },
            new { Number = 3, Text = "recover", Type = WordType.Verb, ObjectiveId = 114 },
            new { Number = 4, Text = "from", Type = WordType.Preposition, ObjectiveId = 114 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 114 },
            new { Number = 6, Text = "lost", Type = WordType.Verb, ObjectiveId = 114 },
            new { Number = 7, Text = "war.", Type = WordType.Noun, ObjectiveId = 114 },

            new { Number = 1, Text = "After", Type = WordType.Preposition, ObjectiveId = 115 },
            new { Number = 2, Text = "computer", Type = WordType.Noun, ObjectiveId = 115 },
            new { Number = 3, Text = "breaking", Type = WordType.Verb, ObjectiveId = 115 },
            new { Number = 4, Text = "down", Type = WordType.Preposition, ObjectiveId = 115 },
            new { Number = 5, Text = "we", Type = WordType.Pronoun, ObjectiveId = 115 },
            new { Number = 6, Text = "could not", Type = WordType.ModalVerb, ObjectiveId = 115 },
            new { Number = 7, Text = "restore", Type = WordType.Verb, ObjectiveId = 115 },
            new { Number = 8, Text = "the", Type = WordType.Determiner, ObjectiveId = 115 },
            new { Number = 9, Text = "lost", Type = WordType.Verb, ObjectiveId = 115 },
            new { Number = 10, Text = "data.", Type = WordType.Noun, ObjectiveId = 115 },

            #endregion

            #region Quest 26

            new { Number = 1, Text = "Having", Type = WordType.PrimaryVerb, ObjectiveId = 116 },
            new { Number = 2, Text = "bought", Type = WordType.Verb, ObjectiveId = 116 },
            new { Number = 3, Text = "some", Type = WordType.Adverb, ObjectiveId = 116 },
            new { Number = 4, Text = "new", Type = WordType.Adjective, ObjectiveId = 116 },
            new { Number = 5, Text = "clothes", Type = WordType.Noun, ObjectiveId = 116 },
            new { Number = 6, Text = "she", Type = WordType.Pronoun, ObjectiveId = 116 },
            new { Number = 7, Text = "felt", Type = WordType.Verb, ObjectiveId = 116 },
            new { Number = 8, Text = "much", Type = WordType.Adverb, ObjectiveId = 116 },
            new { Number = 9, Text = "better.", Type = WordType.ComparisonAdjective, ObjectiveId = 116 },

            new { Number = 1, Text = "Having", Type = WordType.PrimaryVerb, ObjectiveId = 117 },
            new { Number = 2, Text = "looked", Type = WordType.Verb, ObjectiveId = 117 },
            new { Number = 3, Text = "through", Type = WordType.Preposition, ObjectiveId = 117 },
            new { Number = 4, Text = "a", Type = WordType.Determiner, ObjectiveId = 117 },
            new { Number = 5, Text = "lot", Type = WordType.Adverb, ObjectiveId = 117 },
            new { Number = 6, Text = "of", Type = WordType.Preposition, ObjectiveId = 117 },
            new { Number = 7, Text = "journals", Type = WordType.Noun, ObjectiveId = 117 },
            new { Number = 8, Text = "and", Type = WordType.Preposition, ObjectiveId = 117 },
            new { Number = 9, Text = "papers", Type = WordType.Noun, ObjectiveId = 117 },
            new { Number = 10, Text = "he", Type = WordType.Pronoun, ObjectiveId = 117 },
            new { Number = 11, Text = "began", Type = WordType.Verb, ObjectiveId = 117 },
            new { Number = 12, Text = "to", Type = WordType.Preposition, ObjectiveId = 117 },
            new { Number = 13, Text = "write", Type = WordType.Verb, ObjectiveId = 117 },
            new { Number = 14, Text = "his", Type = WordType.Pronoun, ObjectiveId = 117 },
            new { Number = 15, Text = "report.", Type = WordType.Noun, ObjectiveId = 117 },

            new { Number = 1, Text = "Having", Type = WordType.PrimaryVerb, ObjectiveId = 118 },
            new { Number = 2, Text = "taken", Type = WordType.Verb, ObjectiveId = 118 },
            new { Number = 3, Text = "my", Type = WordType.Pronoun, ObjectiveId = 118 },
            new { Number = 4, Text = "advice", Type = WordType.Noun, ObjectiveId = 118 },
            new { Number = 5, Text = "she", Type = WordType.Pronoun, ObjectiveId = 118 },
            new { Number = 6, Text = "followed", Type = WordType.Verb, ObjectiveId = 118 },
            new { Number = 7, Text = "it.", Type = WordType.Pronoun, ObjectiveId = 118 },

            new { Number = 1, Text = "Having", Type = WordType.PrimaryVerb, ObjectiveId = 119 },
            new { Number = 2, Text = "paid", Type = WordType.Verb, ObjectiveId = 119 },
            new { Number = 3, Text = "the", Type = WordType.Determiner, ObjectiveId = 119 },
            new { Number = 4, Text = "fine", Type = WordType.Noun, ObjectiveId = 119 },
            new { Number = 5, Text = "he", Type = WordType.Pronoun, ObjectiveId = 119 },
            new { Number = 6, Text = "did not", Type = WordType.PrimaryVerb, ObjectiveId = 119 },
            new { Number = 7, Text = "break", Type = WordType.Verb, ObjectiveId = 119 },
            new { Number = 8, Text = "the", Type = WordType.Determiner, ObjectiveId = 119 },
            new { Number = 9, Text = "law", Type = WordType.Noun, ObjectiveId = 119 },
            new { Number = 10, Text = "again.", Type = WordType.Adverb, ObjectiveId = 119 },

            new { Number = 1, Text = "Having", Type = WordType.PrimaryVerb, ObjectiveId = 120 },
            new { Number = 2, Text = "sought", Type = WordType.Verb, ObjectiveId = 120 },
            new { Number = 3, Text = "the", Type = WordType.Determiner, ObjectiveId = 120 },
            new { Number = 4, Text = "flat", Type = WordType.Noun, ObjectiveId = 120 },
            new { Number = 5, Text = "they", Type = WordType.Pronoun, ObjectiveId = 120 },
            new { Number = 6, Text = "found", Type = WordType.Verb, ObjectiveId = 120 },
            new { Number = 7, Text = "no", Type = WordType.Adverb, ObjectiveId = 120 },
            new { Number = 8, Text = "evidence.", Type = WordType.Noun, ObjectiveId = 120 },

            #endregion

            #region Quest 28

            new { Number = 1, Text = "Is", Type = WordType.PrimaryVerb, ObjectiveId = 121 },
            new { Number = 2, Text = "the", Type = WordType.Determiner, ObjectiveId = 121 },
            new { Number = 3, Text = "office", Type = WordType.Noun, ObjectiveId = 121 },
            new { Number = 4, Text = "cleaned", Type = WordType.Verb, ObjectiveId = 121 },
            new { Number = 5, Text = "every", Type = WordType.Adverb, ObjectiveId = 121 },
            new { Number = 6, Text = "day?", Type = WordType.Noun, ObjectiveId = 121 },

            new { Number = 1, Text = "I", Type = WordType.Pronoun, ObjectiveId = 122 },
            new { Number = 2, Text = "am", Type = WordType.PrimaryVerb, ObjectiveId = 122 },
            new { Number = 3, Text = "invited", Type = WordType.Verb, ObjectiveId = 122 },
            new { Number = 4, Text = "to", Type = WordType.Preposition, ObjectiveId = 122 },
            new { Number = 5, Text = "a", Type = WordType.Determiner, ObjectiveId = 122 },
            new { Number = 6, Text = "party.", Type = WordType.Noun, ObjectiveId = 122 },

            new { Number = 1, Text = "He", Type = WordType.Pronoun, ObjectiveId = 123 },
            new { Number = 2, Text = "was", Type = WordType.PrimaryVerb, ObjectiveId = 123 },
            new { Number = 3, Text = "offered", Type = WordType.Verb, ObjectiveId = 123 },
            new { Number = 4, Text = "a", Type = WordType.Determiner, ObjectiveId = 123 },
            new { Number = 5, Text = "good", Type = WordType.Adjective, ObjectiveId = 123 },
            new { Number = 6, Text = "job", Type = WordType.Noun, ObjectiveId = 123 },
            new { Number = 7, Text = "at", Type = WordType.Preposition, ObjectiveId = 123 },
            new { Number = 8, Text = "a", Type = WordType.Determiner, ObjectiveId = 123 },
            new { Number = 9, Text = "large", Type = WordType.Adjective, ObjectiveId = 123 },
            new { Number = 10, Text = "construction", Type = WordType.Adjective, ObjectiveId = 123 },
            new { Number = 11, Text = "company.", Type = WordType.Noun, ObjectiveId = 123 },

            new { Number = 1, Text = "Dinner", Type = WordType.Noun, ObjectiveId = 124 },
            new { Number = 2, Text = "is", Type = WordType.PrimaryVerb, ObjectiveId = 124 },
            new { Number = 3, Text = "served", Type = WordType.Verb, ObjectiveId = 124 },
            new { Number = 4, Text = "after", Type = WordType.Preposition, ObjectiveId = 124 },
            new { Number = 5, Text = "seven.", Type = WordType.LetterNumber, ObjectiveId = 124 },

            new { Number = 1, Text = "He", Type = WordType.Pronoun, ObjectiveId = 125 },
            new { Number = 2, Text = "was", Type = WordType.PrimaryVerb, ObjectiveId = 125 },
            new { Number = 3, Text = "followed", Type = WordType.Verb, ObjectiveId = 125 },
            new { Number = 4, Text = "by", Type = WordType.Preposition, ObjectiveId = 125 },
            new { Number = 5, Text = "the", Type = WordType.Determiner, ObjectiveId = 125 },
            new { Number = 6, Text = "police.", Type = WordType.Noun, ObjectiveId = 125 },

            #endregion

            #region Quest 29

            new { Number = 1, Text = "Was", Type = WordType.PrimaryVerb, ObjectiveId = 126 },
            new { Number = 2, Text = "the", Type = WordType.Determiner, ObjectiveId = 126 },
            new { Number = 3, Text = "question", Type = WordType.Noun, ObjectiveId = 126 },
            new { Number = 4, Text = "being", Type = WordType.PrimaryVerb, ObjectiveId = 126 },
            new { Number = 5, Text = "discussed", Type = WordType.Verb, ObjectiveId = 126 },
            new { Number = 6, Text = "at", Type = WordType.Preposition, ObjectiveId = 126 },
            new { Number = 7, Text = "seven", Type = WordType.LetterNumber, ObjectiveId = 126 },
            new { Number = 8, Text = "o'clock?", Type = WordType.Adverb, ObjectiveId = 126 },

            new { Number = 1, Text = "The", Type = WordType.Determiner, ObjectiveId = 127 },
            new { Number = 2, Text = "room", Type = WordType.Noun, ObjectiveId = 127 },
            new { Number = 3, Text = "is", Type = WordType.PrimaryVerb, ObjectiveId = 127 },
            new { Number = 4, Text = "being", Type = WordType.PrimaryVerb, ObjectiveId = 127 },
            new { Number = 5, Text = "cleaned", Type = WordType.Verb, ObjectiveId = 127 },
            new { Number = 6, Text = "at", Type = WordType.Preposition, ObjectiveId = 127 },
            new { Number = 7, Text = "this", Type = WordType.Pronoun, ObjectiveId = 127 },
            new { Number = 8, Text = "moment.", Type = WordType.Noun, ObjectiveId = 127 },

            new { Number = 1, Text = "He", Type = WordType.Pronoun, ObjectiveId = 128 },
            new { Number = 2, Text = "is", Type = WordType.PrimaryVerb, ObjectiveId = 128 },
            new { Number = 3, Text = "being", Type = WordType.PrimaryVerb, ObjectiveId = 128 },
            new { Number = 4, Text = "examined", Type = WordType.Verb, ObjectiveId = 128 },
            new { Number = 5, Text = "by", Type = WordType.Preposition, ObjectiveId = 128 },
            new { Number = 6, Text = "the", Type = WordType.Determiner, ObjectiveId = 128 },
            new { Number = 7, Text = "doctor", Type = WordType.Noun, ObjectiveId = 128 },
            new { Number = 8, Text = "at", Type = WordType.Preposition, ObjectiveId = 128 },
            new { Number = 9, Text = "the", Type = WordType.Determiner, ObjectiveId = 128 },
            new { Number = 10, Text = "moment.", Type = WordType.Noun, ObjectiveId = 128 },

            new { Number = 1, Text = "He", Type = WordType.Pronoun, ObjectiveId = 129 },
            new { Number = 2, Text = "is not", Type = WordType.PrimaryVerb, ObjectiveId = 129 },
            new { Number = 3, Text = "being", Type = WordType.PrimaryVerb, ObjectiveId = 129 },
            new { Number = 4, Text = "followed", Type = WordType.Verb, ObjectiveId = 129 },
            new { Number = 5, Text = "by", Type = WordType.Preposition, ObjectiveId = 129 },
            new { Number = 6, Text = "the", Type = WordType.Determiner, ObjectiveId = 129 },
            new { Number = 7, Text = "police", Type = WordType.Noun, ObjectiveId = 129 },
            new { Number = 8, Text = "at", Type = WordType.Preposition, ObjectiveId = 129 },
            new { Number = 9, Text = "the", Type = WordType.Determiner, ObjectiveId = 129 },
            new { Number = 10, Text = "moment.", Type = WordType.Noun, ObjectiveId = 129 },

            new { Number = 1, Text = "Is", Type = WordType.PrimaryVerb, ObjectiveId = 130 },
            new { Number = 2, Text = "his", Type = WordType.Pronoun, ObjectiveId = 130 },
            new { Number = 3, Text = "house", Type = WordType.Noun, ObjectiveId = 130 },
            new { Number = 4, Text = "being", Type = WordType.PrimaryVerb, ObjectiveId = 130 },
            new { Number = 5, Text = "built", Type = WordType.Verb, ObjectiveId = 130 },
            new { Number = 6, Text = "now?", Type = WordType.Adverb, ObjectiveId = 130 },

            #endregion

            #region Quest 30

            new { Number = 1, Text = "Will", Type = WordType.PrimaryVerb, ObjectiveId = 131 },
            new { Number = 2, Text = "this", Type = WordType.Pronoun, ObjectiveId = 131 },
            new { Number = 3, Text = "book", Type = WordType.Noun, ObjectiveId = 131 },
            new { Number = 4, Text = "have", Type = WordType.PrimaryVerb, ObjectiveId = 131 },
            new { Number = 5, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 131 },
            new { Number = 6, Text = "read", Type = WordType.Verb, ObjectiveId = 131 },
            new { Number = 7, Text = "by", Type = WordType.Preposition, ObjectiveId = 131 },
            new { Number = 8, Text = "two", Type = WordType.LetterNumber, ObjectiveId = 131 },
            new { Number = 9, Text = "o'clock", Type = WordType.Adverb, ObjectiveId = 131 },
            new { Number = 10, Text = "tomorrow?", Type = WordType.Adverb, ObjectiveId = 131 },

            new { Number = 1, Text = "This", Type = WordType.Pronoun, ObjectiveId = 132 },
            new { Number = 2, Text = "book", Type = WordType.Noun, ObjectiveId = 132 },
            new { Number = 3, Text = "has", Type = WordType.PrimaryVerb, ObjectiveId = 132 },
            new { Number = 4, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 132 },
            new { Number = 5, Text = "read.", Type = WordType.Verb, ObjectiveId = 132 },

            new { Number = 1, Text = "The", Type = WordType.Determiner, ObjectiveId = 133 },
            new { Number = 2, Text = "message", Type = WordType.Noun, ObjectiveId = 133 },
            new { Number = 3, Text = "had not", Type = WordType.PrimaryVerb, ObjectiveId = 133 },
            new { Number = 4, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 133 },
            new { Number = 5, Text = "read", Type = WordType.Verb, ObjectiveId = 133 },
            new { Number = 6, Text = "by", Type = WordType.Preposition, ObjectiveId = 133 },
            new { Number = 7, Text = "two", Type = WordType.LetterNumber, ObjectiveId = 133 },
            new { Number = 8, Text = "o'clock.", Type = WordType.Adverb, ObjectiveId = 133 },

            new { Number = 1, Text = "She", Type = WordType.Pronoun, ObjectiveId = 134 },
            new { Number = 2, Text = "has", Type = WordType.PrimaryVerb, ObjectiveId = 134 },
            new { Number = 3, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 134 },
            new { Number = 4, Text = "invited", Type = WordType.Verb, ObjectiveId = 134 },
            new { Number = 5, Text = "to", Type = WordType.Preposition, ObjectiveId = 134 },
            new { Number = 6, Text = "the", Type = WordType.Determiner, ObjectiveId = 134 },
            new { Number = 7, Text = "restaurant.", Type = WordType.Noun, ObjectiveId = 134 },

            new { Number = 1, Text = "Have", Type = WordType.PrimaryVerb, ObjectiveId = 135 },
            new { Number = 2, Text = "the", Type = WordType.Determiner, ObjectiveId = 135 },
            new { Number = 3, Text = "books", Type = WordType.Noun, ObjectiveId = 135 },
            new { Number = 4, Text = "been", Type = WordType.PrimaryVerb, ObjectiveId = 135 },
            new { Number = 5, Text = "sold?", Type = WordType.Verb, ObjectiveId = 135 },

            #endregion
        ];

        return words;
    }

    private static object[] GetQuestObjectives()
    {
        object[] questObjectives =
        [
            new { QuestId = 1, ObjectiveId = 1 },
            new { QuestId = 1, ObjectiveId = 2 },
            new { QuestId = 1, ObjectiveId = 3 },
            new { QuestId = 1, ObjectiveId = 4 },
            new { QuestId = 1, ObjectiveId = 5 },
            new { QuestId = 2, ObjectiveId = 6 },
            new { QuestId = 2, ObjectiveId = 7 },
            new { QuestId = 2, ObjectiveId = 8 },
            new { QuestId = 2, ObjectiveId = 9 },
            new { QuestId = 2, ObjectiveId = 10 },
            new { QuestId = 3, ObjectiveId = 11 },
            new { QuestId = 3, ObjectiveId = 12 },
            new { QuestId = 3, ObjectiveId = 13 },
            new { QuestId = 3, ObjectiveId = 14 },
            new { QuestId = 3, ObjectiveId = 15 },
            new { QuestId = 4, ObjectiveId = 16 },
            new { QuestId = 4, ObjectiveId = 17 },
            new { QuestId = 4, ObjectiveId = 18 },
            new { QuestId = 4, ObjectiveId = 19 },
            new { QuestId = 4, ObjectiveId = 20 },
            new { QuestId = 5, ObjectiveId = 21 },
            new { QuestId = 5, ObjectiveId = 22 },
            new { QuestId = 5, ObjectiveId = 23 },
            new { QuestId = 5, ObjectiveId = 24 },
            new { QuestId = 5, ObjectiveId = 25 },
            new { QuestId = 6, ObjectiveId = 26 },
            new { QuestId = 6, ObjectiveId = 27 },
            new { QuestId = 6, ObjectiveId = 28 },
            new { QuestId = 6, ObjectiveId = 29 },
            new { QuestId = 6, ObjectiveId = 30 },
            new { QuestId = 7, ObjectiveId = 31 },
            new { QuestId = 7, ObjectiveId = 32 },
            new { QuestId = 7, ObjectiveId = 33 },
            new { QuestId = 7, ObjectiveId = 34 },
            new { QuestId = 7, ObjectiveId = 35 },
            new { QuestId = 8, ObjectiveId = 36 },
            new { QuestId = 8, ObjectiveId = 37 },
            new { QuestId = 8, ObjectiveId = 38 },
            new { QuestId = 8, ObjectiveId = 39 },
            new { QuestId = 8, ObjectiveId = 40 },
            new { QuestId = 9, ObjectiveId = 41 },
            new { QuestId = 9, ObjectiveId = 42 },
            new { QuestId = 9, ObjectiveId = 43 },
            new { QuestId = 9, ObjectiveId = 44 },
            new { QuestId = 9, ObjectiveId = 45 },
            new { QuestId = 10, ObjectiveId = 46 },
            new { QuestId = 10, ObjectiveId = 47 },
            new { QuestId = 10, ObjectiveId = 48 },
            new { QuestId = 10, ObjectiveId = 49 },
            new { QuestId = 10, ObjectiveId = 50 },
            new { QuestId = 11, ObjectiveId = 51 },
            new { QuestId = 11, ObjectiveId = 52 },
            new { QuestId = 11, ObjectiveId = 53 },
            new { QuestId = 11, ObjectiveId = 54 },
            new { QuestId = 11, ObjectiveId = 55 },
            new { QuestId = 12, ObjectiveId = 56 },
            new { QuestId = 12, ObjectiveId = 57 },
            new { QuestId = 12, ObjectiveId = 58 },
            new { QuestId = 12, ObjectiveId = 59 },
            new { QuestId = 12, ObjectiveId = 60 },
            new { QuestId = 13, ObjectiveId = 61 },
            new { QuestId = 13, ObjectiveId = 62 },
            new { QuestId = 13, ObjectiveId = 63 },
            new { QuestId = 13, ObjectiveId = 64 },
            new { QuestId = 13, ObjectiveId = 65 },
            new { QuestId = 14, ObjectiveId = 66 },
            new { QuestId = 14, ObjectiveId = 67 },
            new { QuestId = 14, ObjectiveId = 68 },
            new { QuestId = 14, ObjectiveId = 69 },
            new { QuestId = 14, ObjectiveId = 70 },
            new { QuestId = 15, ObjectiveId = 71 },
            new { QuestId = 15, ObjectiveId = 72 },
            new { QuestId = 15, ObjectiveId = 73 },
            new { QuestId = 15, ObjectiveId = 74 },
            new { QuestId = 15, ObjectiveId = 75 },
            new { QuestId = 16, ObjectiveId = 76 },
            new { QuestId = 16, ObjectiveId = 77 },
            new { QuestId = 16, ObjectiveId = 78 },
            new { QuestId = 16, ObjectiveId = 79 },
            new { QuestId = 16, ObjectiveId = 80 },
            new { QuestId = 17, ObjectiveId = 81 },
            new { QuestId = 17, ObjectiveId = 82 },
            new { QuestId = 17, ObjectiveId = 83 },
            new { QuestId = 17, ObjectiveId = 84 },
            new { QuestId = 17, ObjectiveId = 85 },
            new { QuestId = 18, ObjectiveId = 86 },
            new { QuestId = 18, ObjectiveId = 87 },
            new { QuestId = 18, ObjectiveId = 88 },
            new { QuestId = 18, ObjectiveId = 89 },
            new { QuestId = 18, ObjectiveId = 90 },
            new { QuestId = 19, ObjectiveId = 76 },
            new { QuestId = 19, ObjectiveId = 77 },
            new { QuestId = 19, ObjectiveId = 78 },
            new { QuestId = 19, ObjectiveId = 79 },
            new { QuestId = 19, ObjectiveId = 80 },
            new { QuestId = 19, ObjectiveId = 81 },
            new { QuestId = 19, ObjectiveId = 82 },
            new { QuestId = 19, ObjectiveId = 83 },
            new { QuestId = 19, ObjectiveId = 84 },
            new { QuestId = 19, ObjectiveId = 85 },
            new { QuestId = 19, ObjectiveId = 86 },
            new { QuestId = 19, ObjectiveId = 87 },
            new { QuestId = 19, ObjectiveId = 88 },
            new { QuestId = 19, ObjectiveId = 89 },
            new { QuestId = 19, ObjectiveId = 90 },
            new { QuestId = 20, ObjectiveId = 91 },
            new { QuestId = 20, ObjectiveId = 92 },
            new { QuestId = 20, ObjectiveId = 93 },
            new { QuestId = 20, ObjectiveId = 94 },
            new { QuestId = 20, ObjectiveId = 95 },
            new { QuestId = 21, ObjectiveId = 96 },
            new { QuestId = 21, ObjectiveId = 97 },
            new { QuestId = 21, ObjectiveId = 98 },
            new { QuestId = 21, ObjectiveId = 99 },
            new { QuestId = 21, ObjectiveId = 100 },
            new { QuestId = 22, ObjectiveId = 101 },
            new { QuestId = 22, ObjectiveId = 102 },
            new { QuestId = 22, ObjectiveId = 103 },
            new { QuestId = 22, ObjectiveId = 104 },
            new { QuestId = 22, ObjectiveId = 105 },
            new { QuestId = 23, ObjectiveId = 91 },
            new { QuestId = 23, ObjectiveId = 92 },
            new { QuestId = 23, ObjectiveId = 93 },
            new { QuestId = 23, ObjectiveId = 94 },
            new { QuestId = 23, ObjectiveId = 95 },
            new { QuestId = 23, ObjectiveId = 96 },
            new { QuestId = 23, ObjectiveId = 97 },
            new { QuestId = 23, ObjectiveId = 98 },
            new { QuestId = 23, ObjectiveId = 99 },
            new { QuestId = 23, ObjectiveId = 100 },
            new { QuestId = 23, ObjectiveId = 101 },
            new { QuestId = 23, ObjectiveId = 102 },
            new { QuestId = 23, ObjectiveId = 103 },
            new { QuestId = 23, ObjectiveId = 104 },
            new { QuestId = 23, ObjectiveId = 105 },
            new { QuestId = 24, ObjectiveId = 106 },
            new { QuestId = 24, ObjectiveId = 107 },
            new { QuestId = 24, ObjectiveId = 108 },
            new { QuestId = 24, ObjectiveId = 109 },
            new { QuestId = 24, ObjectiveId = 110 },
            new { QuestId = 25, ObjectiveId = 111 },
            new { QuestId = 25, ObjectiveId = 112 },
            new { QuestId = 25, ObjectiveId = 113 },
            new { QuestId = 25, ObjectiveId = 114 },
            new { QuestId = 25, ObjectiveId = 115 },
            new { QuestId = 26, ObjectiveId = 116 },
            new { QuestId = 26, ObjectiveId = 117 },
            new { QuestId = 26, ObjectiveId = 118 },
            new { QuestId = 26, ObjectiveId = 119 },
            new { QuestId = 26, ObjectiveId = 120 },
            new { QuestId = 27, ObjectiveId = 106 },
            new { QuestId = 27, ObjectiveId = 107 },
            new { QuestId = 27, ObjectiveId = 108 },
            new { QuestId = 27, ObjectiveId = 109 },
            new { QuestId = 27, ObjectiveId = 110 },
            new { QuestId = 27, ObjectiveId = 111 },
            new { QuestId = 27, ObjectiveId = 112 },
            new { QuestId = 27, ObjectiveId = 113 },
            new { QuestId = 27, ObjectiveId = 114 },
            new { QuestId = 27, ObjectiveId = 115 },
            new { QuestId = 27, ObjectiveId = 116 },
            new { QuestId = 27, ObjectiveId = 117 },
            new { QuestId = 27, ObjectiveId = 118 },
            new { QuestId = 27, ObjectiveId = 119 },
            new { QuestId = 27, ObjectiveId = 120 },
            new { QuestId = 28, ObjectiveId = 121 },
            new { QuestId = 28, ObjectiveId = 122 },
            new { QuestId = 28, ObjectiveId = 123 },
            new { QuestId = 28, ObjectiveId = 124 },
            new { QuestId = 28, ObjectiveId = 125 },
            new { QuestId = 29, ObjectiveId = 126 },
            new { QuestId = 29, ObjectiveId = 127 },
            new { QuestId = 29, ObjectiveId = 128 },
            new { QuestId = 29, ObjectiveId = 129 },
            new { QuestId = 29, ObjectiveId = 130 },
            new { QuestId = 30, ObjectiveId = 131 },
            new { QuestId = 30, ObjectiveId = 132 },
            new { QuestId = 30, ObjectiveId = 133 },
            new { QuestId = 30, ObjectiveId = 134 },
            new { QuestId = 30, ObjectiveId = 135 },
            new { QuestId = 31, ObjectiveId = 121 },
            new { QuestId = 31, ObjectiveId = 122 },
            new { QuestId = 31, ObjectiveId = 123 },
            new { QuestId = 31, ObjectiveId = 124 },
            new { QuestId = 31, ObjectiveId = 125 },
            new { QuestId = 31, ObjectiveId = 126 },
            new { QuestId = 31, ObjectiveId = 127 },
            new { QuestId = 31, ObjectiveId = 128 },
            new { QuestId = 31, ObjectiveId = 129 },
            new { QuestId = 31, ObjectiveId = 130 },
            new { QuestId = 31, ObjectiveId = 131 },
            new { QuestId = 31, ObjectiveId = 132 },
            new { QuestId = 31, ObjectiveId = 133 },
            new { QuestId = 31, ObjectiveId = 134 },
            new { QuestId = 31, ObjectiveId = 135 }
        ];

        return questObjectives;
    }

    private static object[] GetObjectives()
    {
        object[] objectives =
        [
            new { Id = 1, RusPhrase = "Ты не увидишь." },
            new { Id = 2, RusPhrase = "Мы покажем?" },
            new { Id = 3, RusPhrase = "Она работала?" },
            new { Id = 4, RusPhrase = "Ты не думал." },
            new { Id = 5, RusPhrase = "Я посмотрю?" },
            new { Id = 6, RusPhrase = "Мы не оставили его." },
            new { Id = 7, RusPhrase = "Он поймёт тебя." },
            new { Id = 8, RusPhrase = "Ты открываешь ей." },
            new { Id = 9, RusPhrase = "Я сломаю?" },
            new { Id = 10, RusPhrase = "Я показал им?" },
            new { Id = 11, RusPhrase = "Тебе хочется пить?" },
            new { Id = 12, RusPhrase = "Ей хотелось забывать?" },
            new { Id = 13, RusPhrase = "Я был в музее." },
            new { Id = 14, RusPhrase = "Ей не нравилось показывать." },
            new { Id = 15, RusPhrase = "Она в лифте?" },
            new { Id = 16, RusPhrase = "Он будет их актёром?" },
            new { Id = 17, RusPhrase = "Мы не их бухгалтеры." },
            new { Id = 18, RusPhrase = "Мы были его историками." },
            new { Id = 19, RusPhrase = "Он будет её писателем." },
            new { Id = 20, RusPhrase = "Мы не будем их гидами." },
            new { Id = 21, RusPhrase = "Они учатся на гидов?" },
            new { Id = 22, RusPhrase = "Я не буду менеджером." },
            new { Id = 23, RusPhrase = "Мы учились на менеджеров." },
            new { Id = 24, RusPhrase = "Они работали в компании дизайнерами?" },
            new { Id = 25, RusPhrase = "Ты историк." },
            new { Id = 26, RusPhrase = "Эта ручка не больше той." },
            new { Id = 27, RusPhrase = "Этот телевизор не дорогой." },
            new { Id = 28, RusPhrase = "Этот телефон дешевле того?" },
            new { Id = 29, RusPhrase = "Эта ручка длиннее той." },
            new { Id = 30, RusPhrase = "Эти апельсины самые маленькие?" },
            new { Id = 31, RusPhrase = "Мы будем читать кому-нибудь." },
            new { Id = 32, RusPhrase = "Они чувствуют везде?" },
            new { Id = 33, RusPhrase = "Ты встречаешь везде?" },
            new { Id = 34, RusPhrase = "Он будет знать всех?" },
            new { Id = 35, RusPhrase = "Он никогда не видит." },
            new { Id = 36, RusPhrase = "Она не повернула нас 6 месяцев назад." },
            new { Id = 37, RusPhrase = "Он будет расти через 2 месяца?" },
            new { Id = 38, RusPhrase = "Я расскажу тебе через 6 месяцев." },
            new { Id = 39, RusPhrase = "Ты будешь там через 6 месяцев?" },
            new { Id = 40, RusPhrase = "Она любила их в выходные?" },
            new { Id = 41, RusPhrase = "На полу не было ручек." },
            new { Id = 42, RusPhrase = "Под столом есть ножи." },
            new { Id = 43, RusPhrase = "Под креслом будет мяч." },
            new { Id = 44, RusPhrase = "На столе нет ручки." },
            new { Id = 45, RusPhrase = "Под креслом был стакан?" },
            new { Id = 46, RusPhrase = "Он идёт на станцию?" },
            new { Id = 47, RusPhrase = "Ты не выйдешь из сада." },
            new { Id = 48, RusPhrase = "В комнате не будет шкафов." },
            new { Id = 49, RusPhrase = "На подоконнике будет чашка." },
            new { Id = 50, RusPhrase = "Под столом были стаканы?" },
            new { Id = 51, RusPhrase = "Мы не должны чувствовать." },
            new { Id = 52, RusPhrase = "Нужно мне стоять?" },
            new { Id = 53, RusPhrase = "Мы можем закрыть." },
            new { Id = 54, RusPhrase = "Мы не показали." },
            new { Id = 55, RusPhrase = "Она не должна ответить." },
            new { Id = 56, RusPhrase = "Он читал письмо с 4 до 10 вчера." },
            new { Id = 57, RusPhrase = "Мы отвечали на письмо в 4 вчера." },
            new { Id = 58, RusPhrase = "Ты чувствуешь себя плохо целый день сегодня." },
            new { Id = 59, RusPhrase = "Мы изучали французский целый день вчера." },
            new { Id = 60, RusPhrase = "Ты будешь читать книгу целый день завтра." },
            new { Id = 61, RusPhrase = "Мне холодно?" },
            new { Id = 62, RusPhrase = "У меня не голубые глаза." },
            new { Id = 63, RusPhrase = "Ты здоров." },
            new { Id = 64, RusPhrase = "Вчера было жарко." },
            new { Id = 65, RusPhrase = "Она взрослая." },
            new { Id = 66, RusPhrase = "Беги к ней." },
            new { Id = 67, RusPhrase = "Не поворачивай его тумбочку." },
            new { Id = 68, RusPhrase = "Не бери мой планшет." },
            new { Id = 69, RusPhrase = "Помни про нас." },
            new { Id = 70, RusPhrase = "Пусть он ответит." },
            new { Id = 71, RusPhrase = "Он сбросил вес." },
            new { Id = 72, RusPhrase = "Твой сын просит выключить свет." },
            new { Id = 73, RusPhrase = "Он свалился с пневмонией." },
            new { Id = 74, RusPhrase = "Он взломал дверь." },
            new { Id = 75, RusPhrase = "Цены никогда не падают." },
            new { Id = 76, RusPhrase = "Он только что вышел." },
            new { Id = 77, RusPhrase = "Думаю, что я видел вас где-то." },
            new { Id = 78, RusPhrase = "Я выполнил работу." },
            new { Id = 79, RusPhrase = "Я уже написал письмо своему другу." },
            new { Id = 80, RusPhrase = "Он только что посетил этот супермаркет." },
            new { Id = 81, RusPhrase = "Я не успел закончить проект к началу той недели." },
            new { Id = 82, RusPhrase = "Он сделал домашнее задание до того, как его родители вернулись домой?" },
            new { Id = 83, RusPhrase = "Он уже написал письмо, когда ты вошел?" },
            new { Id = 84, RusPhrase = "Вы вернулись домой до того, как начался дождь." },
            new { Id = 85, RusPhrase = "Когда мы приехали на станцию, поезд уже ушел." },
            new { Id = 86, RusPhrase = "Ученики займут свои места до того, как начнется урок." },
            new { Id = 87, RusPhrase = "Они не успеют выиграть три игры к концу месяца." },
            new { Id = 88, RusPhrase = "Я уже окончу эту работу до того, как вы возвратитесь." },
            new { Id = 89, RusPhrase = "Они еще не отгрузят товар, когда придет ваша телеграмма." },
            new { Id = 90, RusPhrase = "Я не закончу писать доклад к вечеру." },
            new { Id = 91, RusPhrase = "Она готовит ужин уже три часа." },
            new { Id = 92, RusPhrase = "Я пекла этот пирог с самого утра." },
            new { Id = 93, RusPhrase = "Рабочие пытаются сдвинуть наш шкаф с места вот уже полчаса." },
            new { Id = 94, RusPhrase = "Я читаю этот журнал с тех пор, как купил его неделю назад." },
            new { Id = 95, RusPhrase = "Я жду свою маму уже давно." },
            new { Id = 96, RusPhrase = "С теп пор показатель быстро рос." },
            new { Id = 97, RusPhrase = "Его руки были грязные, он копал." },
            new { Id = 98, RusPhrase = "Они разговаривали больше часа, до того, как он пришел." },
            new { Id = 99, RusPhrase = "Был час ночи, а соседская собака лаяла уже 2 часа." },
            new { Id = 100, RusPhrase = "Моя собака играла уже полчаса перед тем, как мы пошли гулять." },
            new { Id = 101, RusPhrase = "Они будут разговаривать уже свыше часа, к тому моменту, когда приедет он." },
            new { Id = 102, RusPhrase = "К первому июня он будет работать на этом заводе уже двадцать лет." },
            new { Id = 103, RusPhrase = "К следующему рождеству я уже буду преподавать 10 лет." },
            new { Id = 104, RusPhrase = "Ты будешь ждать свыше двух часов, прежде чем ее самолет, наконец, приземлится?" },
            new { Id = 105, RusPhrase = "В следующем месяце мы будем жить вместе уже 15 лет." },
            new { Id = 106, RusPhrase = "Они будут разговаривать уже свыше часа, к тому моменту, когда приедет он." },
            new { Id = 107, RusPhrase = "К первому июня он будет работать на этом заводе уже двадцать лет." },
            new { Id = 108, RusPhrase = "К следующему рождеству я уже буду преподавать 10 лет." },
            new { Id = 109, RusPhrase = "Ты будешь ждать свыше двух часов, прежде чем ее самолет, наконец, приземлится?" },
            new { Id = 110, RusPhrase = "В следующем месяце мы будем жить вместе уже 15 лет." },
            new { Id = 111, RusPhrase = "Я был разочарован услышать, что все больше и больше людей теряют свою работу." },
            new { Id = 112, RusPhrase = "Измученный, он провалился в сон." },
            new { Id = 113, RusPhrase = "Прибор, купленный в специализированном магазине, имеет гарантию." },
            new { Id = 114, RusPhrase = "Они не смогли оправиться от проигранной войны." },
            new { Id = 115, RusPhrase = "После компьютерного сбоя мы не смогли восстановить потерянные данные." },
            new { Id = 116, RusPhrase = "Купив немного новой одежды, она почувствовала себя намного лучше." },
            new { Id = 117, RusPhrase = "Посмотрев много журналов и газет, он начал писать свой доклад." },
            new { Id = 118, RusPhrase = "Приняв мой совет, она последовала ему." },
            new { Id = 119, RusPhrase = "Заплатив штраф, он больше не нарушал закон." },
            new { Id = 120, RusPhrase = "Обыскав квартиру, они не не нашли никаких доказательств." },
            new { Id = 121, RusPhrase = "Офис убирают каждый день?" },
            new { Id = 122, RusPhrase = "Я приглашён на вечеринку." },
            new { Id = 123, RusPhrase = "Ему предложили хорошую работу в большой строительной компании." },
            new { Id = 124, RusPhrase = "Ужин подается после семи." },
            new { Id = 125, RusPhrase = "Его преследовала полиция." },
            new { Id = 126, RusPhrase = "Вопрос обсуждался в семь часов?" },
            new { Id = 127, RusPhrase = "Комнату в этот момент моют." },
            new { Id = 128, RusPhrase = "Его осматривает доктор в настоящий момент." },
            new { Id = 129, RusPhrase = "Его не преследует полиция в данный момент." },
            new { Id = 130, RusPhrase = "Его дом строится сейчас?" },
            new { Id = 131, RusPhrase = "Эта книга уже будет прочитана завтра к 2 часам?" },
            new { Id = 132, RusPhrase = "Эта книга уже прочитана." },
            new { Id = 133, RusPhrase = "Сообщение еще не было прочитано к двум часам." },
            new { Id = 134, RusPhrase = "Ее уже пригласили в ресторан." },
            new { Id = 135, RusPhrase = "Книги уже распроданы?" }
        ];

        return objectives;
    }

    private static object[] GetQuests()
    {
        object[] quests =
        [
            new { Id = 1, Name = "Базовая форма глагола" },
            new { Id = 2, Name = "Местоимения. Вопросительные слова" },
            new { Id = 3, Name = "Глагол to be. Предлоги места. Like/Want" },
            new { Id = 4, Name = "Притяжательные местоимения" },
            new { Id = 5, Name = "Профессии. Этикет" },
            new { Id = 6, Name = "Степени сравнения прилагательных. Указательные местоимения" },
            new { Id = 7, Name = "Слова-параметры. Употребление muck и many, little и few" },
            new { Id = 8, Name = "Предлоги и параметры времени" },
            new { Id = 9, Name = "There is / There are" },
            new { Id = 10, Name = "Предлоги направления и движения" },
            new { Id = 11, Name = "Модальные глаголы can, must, should" },
            new { Id = 12, Name = "Continuous" },
            new { Id = 13, Name = "Описание людей. Погода" },
            new { Id = 14, Name = "Повелительное наклонение" },
            new { Id = 15, Name = "Фразовые глаголы" },
            new { Id = 16, Name = "Present Perfect" },
            new { Id = 17, Name = "Past Perfect" },
            new { Id = 18, Name = "Future Perfect" },
            new { Id = 19, Name = "Perfect Tenses" },
            new { Id = 20, Name = "Present Perfect Continuous" },
            new { Id = 21, Name = "Past Perfect Continuous" },
            new { Id = 22, Name = "Future Perfect Continuous" },
            new { Id = 23, Name = "Perfect Continuous Tenses" },
            new { Id = 24, Name = "Present Participle Simple" },
            new { Id = 25, Name = "Past Participle" },
            new { Id = 26, Name = "Present Participle Perfect" },
            new { Id = 27, Name = "The Participle" },
            new { Id = 28, Name = "Simple Passive" },
            new { Id = 29, Name = "Continuous Passive" },
            new { Id = 30, Name = "Perfect Passive" },
            new { Id = 31, Name = "Passive Voice" },
        ];

        return quests;
    }
}
