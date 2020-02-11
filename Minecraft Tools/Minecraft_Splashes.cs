using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    internal static class Minecraft_Splashes
    {
        /// <summary>
        /// This matrix contains all the Minecraft splashes, even the ones seen after a game crash or old ones. Note: a few splashes have been modified or simplified since this version doesn't support Unicode characters yet.
        /// </summary>
        internal static List<string> Lista_Líneas = new List<string>(new string[]
        {
            /*"missingno", // Default value extracted from the Minecraft 1.12 source code. This name was also used by a Pokémon on it's first games, but it was shown probably as a game bug.
            "Merry X-mas!", // Shown on 24th of February.
            "Happy new year!", // Shown on 1st of January.
            "OOoooOOOoooo! Spooky!", // Shown on 31st of October.
            "As seen on TV!",
            "Awesome!",
            "100% pure!",
            "May contain nuts!",
            "More polygons!",
            "Sexy!",
            "Limited edition!",
            "Flashing letters!",
            "Made by Notch!",
            "It's here!",
            "Best in class!",
            "It's finished!",
            "Kind of dragon free!",
            "Excitement!",
            "More than 500 sold!",
            "One of a kind!",
            "Heaps of hits on YouTube!",
            "Indev!",
            "Spiders everywhere!",
            "Check it out!",
            "Holy cow, man!",
            "It's a game!",
            "Made in Sweden!",
            "Uses LWJGL!",
            "Reticulating splines!",
            "Minecraft!",
            "Yaaay!",
            "Singleplayer!",
            "Keyboard compatible!",
            "Undocumented!",
            "Ingots!",
            "Exploding creepers!",
            "That's no moon!",
            "l33t!",
            "Create!",
            "Survive!",
            "Dungeon!",
            "Exclusive!",
            "The bee's knees!",
            "Down with O.P.P.!",
            "Closed source!",
            "Classy!",
            "Wow!",
            "Not on steam!",
            "Oh man!",
            "Awesome community!",
            "Pixels!",
            "Teetsuuuuoooo!",
            "Kaaneeeedaaaa!",
            "Now with difficulty!",
            "Enhanced!",
            "90% bug free!",
            "Pretty!",
            "12 herbs and spices!",
            "Fat free!",
            "Absolutely no memes!",
            "Free dental!",
            "Ask your doctor!",
            "Minors welcome!",
            "Cloud computing!",
            "Legal in Finland!",
            "Hard to label!",
            "Technically good!",
            "Bringing home the bacon!",
            "Indie!",
            "GOTY!",
            "Ceci n'est pas une title screen!",
            "Euclidian!",
            "Now in 3D!",
            "Inspirational!",
            "Herregud!",
            "Complex cellular automata!",
            "Yes, sir!",
            "Played by cowboys!",
            "OpenGL 2.1 (if supported)!",
            "Thousands of colors!",
            "Try it!",
            "Age of Wonders is better!",
            "Try the mushroom stew!",
            "Sensational!",
            "Hot tamale, hot hot tamale!",
            "Play him off, keyboard cat!",
            "Guaranteed!",
            "Macroscopic!",
            "Bring it on!",
            "Random splash!",
            "Call your mother!",
            "Monster infighting!",
            "Loved by millions!",
            "Ultimate edition!",
            "Freaky!",
            "You've got a brand new key!",
            "Water proof!",
            "Uninflammable!",
            "Whoa, dude!",
            "All inclusive!",
            "Tell your friends!",
            "NP is not in P!",
            "Music by C418!",
            "Livestreamed!",
            "Haunted!",
            "Polynomial!",
            "Terrestrial!",
            "All is full of love!",
            "Full of stars!",
            "Scientific!",
            "Not as cool as Spock!",
            "Collaborate and listen!",
            "Never dig down!",
            "Take frequent breaks!",
            "Not linear!",
            "Han shot first!",
            "Nice to meet you!",
            "Buckets of lava!",
            "Ride the pig!",
            "Larger than Earth!",
            "sqrt(-1) love you!",
            "Phobos anomaly!",
            "Punching wood!",
            "Falling off cliffs!",
            "1% sugar!",
            "150% hyperbole!",
            "Synecdoche!",
            "Let's danec!",
            "Seecret Friday update!",
            "Reference implementation!",
            "Lewd with two dudes with food!",
            "Kiss the sky!",
            "20 GOTO 10!",
            "Verlet intregration!",
            "Peter Griffin!",
            "Do not distribute!",
            "Cogito ergo sum!",
            "4815162342 lines of code!",
            "A skeleton popped out!",
            "The Work of Notch!",
            "The sum of its parts!",
            "BTAF used to be good!",
            "I miss ADOM!",
            "umop-apisdn!",
            "OICU812!",
            "Bring me Ray Cokes!",
            "Finger-licking!",
            "Thematic!",
            "Pneumatic!",
            "Sublime!",
            "Octagonal!",
            "Une baguette!",
            "Gargamel plays it!",
            "Rita is the new top dog!",
            "SWM forever!",
            "Representing Edsbyn!",
            "Matt Damon!",
            "Supercalifragilisticexpialidocious!",
            "Consummate V's!",
            "Cow Tools!",
            "Double buffered!",
            "Fan fiction!",
            "Flaxkikare!",
            "Jason! Jason! Jason!",
            "Hotter than the sun!",
            "Internet enabled!",
            "Autonomous!",
            "Engage!",
            "Fantasy!",
            "DRR! DRR! DRR!",
            "Kick it root down!",
            "Regional resources!",
            "Woo, facepunch!",
            "Woo, somethingawful!",
            "Woo, /v/!",
            "Woo, tigsource!",
            "Woo, minecraftforum!",
            "Woo, worldofminecraft!",
            "Woo, reddit!",
            "Woo, 2pp!",
            "Google anlyticsed!",
            "Now supports åäö!",
            "Give us Gordon!",
            "Tip your waiter!",
            "Very fun!",
            "12345 is a bad password!",
            "Vote for net neutrality!",
            "Lives in a pineapple under the sea!",
            "MAP11 has two names!",
            "Omnipotent!",
            "Gasp!",
            "...!",
            "Bees, bees, bees, bees!",
            "Jag känner en bot!",
            "This text is hard to read if you play the game at the default resolution, but at 1080p it's fine!",
            "Haha, LOL!",
            "Hampsterdance!",
            "Switches and ores!",
            "Menger sponge!",
            "idspispopd!",
            "Eple (original edit)!",
            "So fresh, so clean!",
            "Slow acting portals!",
            "Try the Nether!",
            "Don't look directly at the bugs!",
            "Oh, ok, Pigmen!",
            "Finally with ladders!",
            "Scary!",
            "Play Minecraft, Watch Topgear, Get Pig!",
            "Twittered about!",
            "Jump up, jump up, and get down!",
            "Joel is neat!",
            "A riddle, wrapped in a mystery!",
            "Huge tracts of land!",
            "Welcome to your Doom!",
            "Stay a while, stay forever!",
            "Stay a while and listen!",
            "Treatment for your rash!",
            "\"Autological\" is!",
            "Information wants to be free!",
            "\"Almost never\" is an interesting concept!",
            "Lots of truthiness!",
            "The creeper is a spy!",
            "Turing complete!",
            "It's groundbreaking!",
            "Let our battle's begin!",
            "The sky is the limit!",
            "Jeb has amazing hair!",
            "Ryan also has amazing hair!",
            "Casual gaming!",
            "Undefeated!",
            "Kinda like Lemmings!",
            "Follow the train, CJ!",
            "Leveraging synergy!",
            "This message will never appear on the splash screen, isn't that weird?",
            "DungeonQuest is unfair!",
            "110813!",
            "90210!",
            "Check out the far lands!",
            "Tyrion would love it!",
            "Also try VVVVVV!",
            "Also try Super Meat Boy!",
            "Also try Terraria!",
            "Also try Mount And Blade!",
            "Also try Project Zomboid!",
            "Also try World of Goo!",
            "Also try Limbo!",
            "Also try Pixeljunk Shooter!",
            "Also try Braid!",
            "That's super!",
            "Bread is pain!",
            "Read more books!",
            "Khaaaaaaaaan!",
            "Less addictive than TV Tropes!",
            "More addictive than lemonade!",
            "Bigger than a bread box!",
            "Millions of peaches!",
            "Fnord!",
            "This is my true form!",
            "Totally forgot about Dre!",
            "Don't bother with the clones!",
            "Pumpkinhead!",
            "Hobo humping slobo babe!",
            "Made by Jeb!",
            "Has an ending!",
            "Finally complete!",
            "Feature packed!",
            "Boots with the fur!",
            "Stop, hammertime!",
            "Testificates!",
            "Conventional!",
            "Homeomorphic to a 3-sphere!",
            "Doesn't avoid double negatives!",
            "Place ALL the blocks!",
            "Does barrel rolls!",
            "Meeting expectations!",
            "PC gaming since 1873!",
            "Ghoughpteighbteau tchoghs!",
            "Déjà vu!",
            "Déjà vu!", // This duplicate is official also in Minecraft, hence it's name.
            "Got your nose!",
            "Haley loves Elan!",
            "Afraid of the big, black bat!",
            "Doesn't use the U-word!",
            "Child's play!",
            "See you next Friday or so!",
            "From the streets of Södermalm!",
            "150 bpm for 400000 minutes!",
            "Technologic!",
            "Funk soul brother!",
            "Pumpa kungen!",
            "?????!", // 日本ハロー！
            "?? ?????!", // 한국 안녕하세요!
            "Helo Cymru!",
            "Czesc Polsko!", // Cześć Polsko!
            "????!", // 你好中国！
            "Npnbet Poccnr!", // Привет Россия!
            "Γeiα σou EAAaδα!", // Γεια σου Ελλάδα!
            "My life for Aiur!",
            "Lennart lennart = new Lennart();",
            "I see your vocabulary has improved!",
            "Who put it there?",
            "You can't explain that!",
            "if not ok then return end",
            "Colormatic", // §1C§2o§3l§4o§5r§6m§7a§8t§9i§ac
            "FUNKY LOL", // §kFUNKY LOL
            "Big Pointy Teeth!",
            "Bekarton guards the gate!",
            "Mmmph, mmph!",
            "Don't feed avocados to parrots!",
            "Swords for everyone!",
            "Plz reply to my tweet!",
            ".party()!",
            "Take her pillow!",
            "Put that cookie down!",
            "Pretty scary!",
            "I have a suggestion.",
            "Now with extra hugs!",
            "Now Java 8!",
            "Woah.",
            "HURNERJSGER?",
            "What's up, Doc?",
            "Now contains 32 random daily cats!",
            "That's Numberwang!",
            "pls rt",
            "Do you want to join my server?",
            "Put a little fence around it!",
            "Throw a blanket over it!",
            "One day, somewhere in the future, my work will be quoted!",
            "Now with additional stuff!",
            "Extra things!",
            "Yay, puppies for everyone!",
            "So sweet, like a nice bon bon!",
            "Popping tags!",
            "Very influential in its circle!",
            "Now With Multiplayer!",
            "Rise from your grave!",
            "Warning! A huge battleship \"STEVE\" is approaching fast!",
            "Blue warrior shot the food!",
            "Run, coward! I hunger!",
            "Flavor with no seasoning!",
            "Strange, but not a stranger!",
            "Tougher than diamonds, rich like cream!",
            "Getting ready to show!",
            "Getting ready to know!",
            "Getting ready to drop!",
            "Getting ready to shock!",
            "Getting ready to freak!",
            "Getting ready to speak!",
            "It swings, it jives!",
            "Cruising streets for gold!",
            "Take an eggbeater and beat it against a skillet!",
            "Make me a table, a funky table!",
            "Take the elevator to the mezzanine!",
            "Stop being reasonable, this is the Internet!",
            "/give @a hugs 64",
            "This is good for Realms.",
            "Any computer is a laptop if you're brave enough!",
            "Do it all, everything!",
            "Where there is not light, there can spider!",
            "GNU Terry Pratchett",
            "More Digital!",
            "doot doot",
            "Falling with style!",
            "There's no stopping the Trollmaso",
            "Throw yourself at the ground and miss",
            "Rule #1: it's never my fault",
            "Replaced molten cheese with blood?",
            "Absolutely fixed relatively broken coordinates",
            "Boats FTW",
            "Javalicious edition",
            "Should not be played while driving",
            "You're going too fast!",
            "Don't feed chocolate to parrots!",
            "The true meaning of covfefe",
            "An illusion! What are you hiding?",
            "Something's not quite right...",
            "Thank you for the fish!",
            "All rumors are true!",
            "Truly gone fishing!",

            // Splashes only shown on game crashes:
            "Who set us up the TNT?",
            "Everything's going to plan. No, really, that was supposed to happen.",
            "Uh... Did I do that?",
            "Oops.",
            "Why did you do that?",
            "I feel sad now :(",
            "My bad.",
            "I'm sorry, Dave.",
            "I let you down. Sorry :(",
            "On the bright side, I bought you a teddy bear!",
            "Daisy, daisy...",
            "Oh - I know what I did wrong!",
            "Hey, that tickles! Hehehe!",
            "I blame Dinnerbone.",
            "You should try our sister game, Minceraft!",
            "Don't be sad. I'll do better next time, I promise!",
            "Don't be sad, have a hug! <3",
            "I just don't know what went wrong :(",
            "Shall we play a game?",
            "Quite honestly, I wouldn't worry myself about that.",
            "I bet Cylons wouldn't have this problem.",
            "Sorry :(",
            "Surprise! Haha. Well, this is awkward.",
            "Would you like a cupcake?",
            "Hi. I'm Minecraft, and I'm a crashaholic.",
            "Ooh. Shiny.",
            "This doesn't make any sense!",
            "Why is it breaking :(",
            "Don't do that.",
            "Ouch. That hurt :(",
            "You're mean.",
            "This is a token for 1 free hug. Redeem at your nearest Mojangsta: [~~HUG~~]",
            "There are four lights!",
            "But it works on my machine.",

            // Splashes only shown on Minecraft Bedrock Edition:
            "Ported Implementation!",
            "100% more yellow text!",
            "flowers more important than grass",
            "Dramatic lighting!",
            "Pocket!",
            "Touch compatible!",
            "Annoying touch buttons!",
            "Uses C++!",
            "Almost C++14!",
            "OpenGL ES 2.0+!",
            "Multithreaded!",
            "D",
            "Haha, LEL!",
            "Play Minecraft: PE, Watch Topgear, Get Pig!",
            "Quite Indie!",
            "!!!1!",
            "DEK was here",
            "Hmmmrmm.",
            "V-synched!",
            "0xffff-1 chunks",
            "Open-world alpha sandbox!",
            "Endless!",
            "1 star! Deal with it notch!",
            "Ask your mother!",
            "Episode 3!",
            "Toilet friendly!",
            "[snapshot intensifies]!",
            "Cubism!",
            "minecraftApp!",
            "Less polygons!",
            "Now with skins!",
            "Blame shogchips",
            "Glowing creepy eyes!",
            "Also try Monument Valley!",
            "MCPE!",
            "& Knuckles!",
            "BAYKN",
            "code.org/minecraft",

            // Splashes shown on special occasions:
            "1K in 24h!",
            "[DO NOT DISTRIBUTE!]",
            "Finally beta!",
            "PC Gamer Demo!",
            "Minecraft is love, Minecraft is life",
            "APRIL FOOLS!",
            "Beta!!!",
            "Cleaner!",
            "Simplified!",
            "Perfected!",
            "Art directed! (by robots)",
            "Machine learned textures!",
            "Not blurry!",
            "Not bubbly!",
            "Not cartoony!",
            "Photo realistic!",
            "Hand painted!",

            // Splashes currently changed:
            "700+ hits on YouTube!",
            "Absolutely dragon free!",
            "Almost java 6!",
            "Now java 6!",
            "Now Java 6!",
            "Coming soon!",
            "More than 5000 sold!",
            "More than 25000 sold!",
            "Notch <3 Ez!",
            "\"Noun\" is an autonym!",
            "Now supports ÅÄÖ!",
            "OpenGL 1.1!",
            "OpenGL 1.2!",
            "Rude with two dudes with food!",
            "Czesc Polska!", // Cześć Polska!
            "SOPA means LOSER in Swedish",
            "Superfragilisticexpialidocious!",
            "That's not a moon!",
            "Tyrian would love it!",
            "When it's finished!",
            "This is good for realms.",
            "Cooler than Spock!",
            "0% sugar!",

            // Splashes currently changed from Minecraft Bedrock Edition:
            "Now with 100% more yellow text!",
            "One star! Deal with it notch!",
            "Play minecraftApp, Watch Topgear, Get Pig!",
            "OpenGL ES 1.1!",
            "Almost C++11!",

            // "Colormatic" splash repeated 16 extra times, to get more chances of rainbow colors:
            "Colormatic",
            "Colormatic",
            "Colormatic",
            "Colormatic",
            "Colormatic",
            "Colormatic",
            "Colormatic",
            "Colormatic",
            "Colormatic",
            "Colormatic",
            "Colormatic",
            "Colormatic",
            "Colormatic",
            "Colormatic",
            "Colormatic",
            "Colormatic",

            // Splashes based on the 256 Minecraft characters in groups of 16 characters:
            "\u00c0\u00c1\u00c2\u00c8\u00ca\u00cb\u00cd\u00d3\u00d4\u00d5\u00da\u00df\u00e3\u00f5\u011f\u0130",
            "\u0131\u0152\u0153\u015e\u015f\u0174\u0175\u017e\u0207\u0000\u0000\u0000\u0000\u0000\u0000\u0000",
            " !\"#$%&'()*+,-.",
            "/0123456789:;<=>?",
            "@ABCDEFGHIJKLMNO",
            "PQRSTUVWXYZ[\\]^_",
            "`abcdefghijklmno",
            "pqrstuvwxyz{|}~\u0000",
            "\u00c7\u00fc\u00e9\u00e2\u00e4\u00e0\u00e5\u00e7\u00ea\u00eb\u00e8\u00ef\u00ee\u00ec\u00c4\u00c5",
            "\u00c9\u00e6\u00c6\u00f4\u00f6\u00f2\u00fb\u00f9\u00ff\u00d6\u00dc\u00f8\u00a3\u00d8\u00d7\u0192",
            "\u00e1\u00ed\u00f3\u00fa\u00f1\u00d1\u00aa\u00ba\u00bf\u00ae\u00ac\u00bd\u00bc\u00a1\u00ab\u00bb",
            "\u2591\u2592\u2593\u2502\u2524\u2561\u2562\u2556\u2555\u2563\u2551\u2557\u255d\u255c\u255b\u2510",
            "\u2514\u2534\u252c\u251c\u2500\u253c\u255e\u255f\u255a\u2554\u2569\u2566\u2560\u2550\u256c\u2567",
            "\u2568\u2564\u2565\u2559\u2558\u2552\u2553\u256b\u256a\u2518\u250c\u2588\u2584\u258c\u2590\u2580",
            "\u03b1\u03b2\u0393\u03c0\u03a3\u03c3\u03bc\u03c4\u03a6\u0398\u03a9\u03b4\u221e\u2205\u2208\u2229",
            "\u2261\u00b1\u2265\u2264\u2320\u2321\u00f7\u2248\u00b0\u2219\u00b7\u221a\u207f\u00b2\u25a0\u0000",*/
            
            // Hermitcraft custom splashes:
            "Hermitcraft!",
            "Hermits!",
            "Hermitpack!",
            "Monsterpack!",
            "Derp counter!",
            "Myth busting!",
            "Evil X!",
            "Cheaty zoom key!",
            "Ninja creepers!",
            "Tag: you're it!",
            "Allium alliance!",
            "Victoria!",
            "Carol!",
            "Sugarcane pillar!",
            "Foolcraft!",
            "Foolcraft 2!",
            "Foolcraft 3!",
            "BdoubleO100!",
            "Biffa2001!",
            "ZombieCleo!",
            "cubfan135!",
            "Docm77!",
            "Etho!",
            "falsesymmetry!",
            "Grian!",
            "hypnotizd!",
            "iJevin!",
            "impulseSV!",
            "iskall85!",
            "Jessassin!",
            "joehillssays!",
            "Keralis1!",
            "MumboJumbo!",
            "PythonGB!",
            "Renthedog!",
            "GoodTimeWithScar!",
            "Stressmonster101!",
            "TangoTek!",
            "Tinfoilchef!",
            "VintageBeef!",
            "Welsknight!",
            "xBCrafted!",
            "Xisumavoid!",
            "Zedaph!",
            "http://www.xisumavoid.com/",

            // Splashes from Monster High:
            "Monster High!",
            "Be yourself, be unique, be a monster!",
            "Freaky just got fabulous!",
            "Drop dead gorgeous!",
            "How do you boo?",
            "Blurple!",
            "Fangtastic!",
            "Vegetarian vampire!",
            "Key W9189!",
            "Ever after high!",
            "Enchantimals!",

             // Monster High lines or collections:
            "13 wishes!",
            "Boo york, Boo york!",
            "Down under ghouls!",
            "Freak du chic!",
            "Freaky fusion!",
            "Ghoul spirit!",
            "Ghoul-to-bat!",
            "Monster exchange!",
            "Monster family!",
            "Scaris city of frights!",
            "Sweet 1600!",
            "Welcome to Monster High!",

            // Monster High movies:
            //"13 wishes!",
            //"Boo york, Boo york!",
            "Electrified!",
            "Escape from skull shores!",
            //"Freaky fusion!",
            "Friday night frights!",
            "Frights, camera, action!",
            "From fear to eternity!",
            "Ghouls rule!",
            "Great scarrier reef!",
            "Haunted!",
            "New ghoul at school!",
            //"Scaris city of frights!",
            //"Welcome to Monster High!",
            "Why do ghouls fall in love!",
            
            // Monster High dolls I own:
            "Draculaura!", // Welcome to Monster High, Ghoul-to-bat
            "Mouscedes King!", // Boo York, Boo York
            "Lagoona Blue!", // Welcome to Monster High
            "Fangelica VanBat!", // Monster family
            "Dracubecca!", // Freaky fusion
            "Luna Mothews!", // Boo York, Boo York
            "Twyla!", // 13 wishes
            "Dustin!", // 13 wishes
            "Rochelle Goyle!", // Scaris city of frights
            "Draculaura!", // Monster exchange
            "Lorna McNessie!", // Monster exchange
            "Elle Eedee!", // Boo York, Boo York
            "Frankie Stein!", // Freak du chic
            "Posea Reef!", // Down under ghouls
            "Venus McFlytrap!", // Ghoul spirit
            "Draculaura!", // Sweet 1600
            "Draculaura!", // Original
            "Count Fabulous!", // Original
            "Marisol Coxi!", // Monster exchange

            // Jupisoft custom splashes:
            "Made by Jupisoft!",
            "The Work of Júpiter Mauro!",
            "Jupisoft = Júpiter Software!",
            "Made in C#!",
            "Júpiter: Mundo x4 Templos!",
            "Seed -488963417782474398 for MC 1.6.4!",
            "The seed 4 always gave ocean!",
            "Try the seed 8!",
            "Seed 6176145511513668659 for MC 1.13.1!",
            "Dungeons!",
            "http://jupisoft.x10host.com/",
            "https://www.youtube.com/channel/UCQkgl08FknXRlP1zVBYSw2A",
            "C D E F G A B!",
            "Do Re Mi Fa Sol La Si!",
            "Harmony!",
            "Musical notes!",
            "David Maeso!",
            "Fratelli Stellari!",
            "Brad Breeck!",
            "Watch Monster High!",
            "Watch Gravity Falls!",
            "Watch Steven Universe!",
            "Watch Star vs. the forces of evil!",
            "On KissCartoon you'll find series!",
            "Bill Cipher!",
            "Journal 1!",
            "Journal 2!",
            "Journal 3!",
            "Caesar cipher!",
            "ZHOFRPH WR JUDYLWB IDOOV!",
            "Travel 3 letters back!",
            "A1Z26 cipher!",
            "9-20 23-15-18-11-19 6-15-18 16-9-9-9-9-9-9-9-9-9-9-9-9-9-9-9-9-9-7-19!",
            "Atbash cipher!",
            "Blf ziv gsv tivzgvhg nbhgvib sfmgvi!",
            "Vigenère cipher!",
            "Extraterrestrial!",
            "Floating city!",
            "Made with Microsoft Visual Studio!",
            "Self-programming applications!",
            "Programming since 2004!",
            "The ancient knowledge was better!",
            "The source code from the Universe itself!",
            "From above the clouds!",
            "Made in Spain!",
            "From Spain with love!",
            "Lenguaje Español!",
            "Anti-gravity!",
            "Atlantis!",
            "Atlantis, from the creators of the Floating city!",
            "Laputa!",
            "Read Gulliver's Travels!",
            "Jonathan Swift!",
            "Peace, light, love!",
            "Beyond science fiction!",
            "Beyond the physical world!",
            "Beyond the emotional world!",
            "From the mind world!",
            "Only at Minecraft Forum!",
            "Only at GitHub!",
            "Magnetism is the key!",
            "The number 3 is the key!",
            "3 imperfect parts make 1 perfect!",
            "Born on day 111!",
            "Born on day 21!",
            "Electronic Fusion!",
            "Gravity depends on acceleration, not on mass!",
            "Randomness is an illusion!",
            "Everything is always moving!",
            "Everything is always spying!",
            "Everything has been invented before!",
            "Rediscovering things!",
            "Monster High tells our ancient history!",
            "Steven Universe tells our ancient history!",
            "Gravity Falls tells our ancient history!",
            "Star vs. the forces of evil tells our ancient history!",
            "In the space, the Sun and the stars glow at once!",
            "Try this bass: A A# G A A G A# A!",
            "Too much wars!",
            "Respecting all life forms!",
            "Only good vibrations!",
            "Only high vibrations!",
            "Uniting humanity!",
            "Minecraft works like our mind!",
            "From between Mars and Jupiter!",
            "100% vegetarian!",
            "100% peaceful!",
            "99% silent!",
            "At the same version of Minecraft!", //"Always at version 1.0!",
            "All-in-one!",
            "Remembering past lives!",
            "Death is an illusion!",
            "Mind over matter!",
            "Wake up!",
            "Living in the space!",
            "Living in the clouds!",
            "Thank you!",
            "Please!",
            "Pretty please!",
            "You know more than you think!",
            "Keep trying!",
            "If I told a story, will you listen?",
            "But no one remembers today...",
            "About all the ancient wars...",
            "Even a planet was destroyed...",
            "The survivors came to Earth...",
            "They built a city in the sky...",
            "They gave knowledge to mankind...",
            "The corruption spread to the world...",
            "The Great Flood destroyed almost everything...",
            "Some were against that decision, so...",
            "A civil war started on the Earth...",
            "Between the so called \"Gods\"...",
            "The floating city was destroyed...",
            "It was made of huge hexagonal blocks...",
            "And were kept together with magnetism...",
            "Which was also spreading the anti-gravity...",
            "By using 2 polarities with high energy...",
            "On it's center it was the main machine...",
            "But all the blocks ended under the ocean...",
            "And all the knowledge was lost...",
            "Gulliver's Travels tells the truth...",
            "About our forgotten history...",
            "Only the ones who were there can remember...",
            "It's also like Jules Verne works...",
            "Do you remember anything about it?",
            "Matter transmutor?!",
            "Infiniscope?!",
            "Also try The Sims 4!",
            "Also try Monster High New Ghoul in School!",
            "Also try Highway Pursuit!",
            "Also try Alice Madness Returns!",
            "Castles in the sky!",
            "Building castles in the sky!",
            "Card castles in the sky!",
            "Jason Tai - Card castles in the sky!",
            "Also try Shantae Half Genie Hero!",
            "Also try FL Studio!",
            "Also try Grand Theft Auto!",
            "Also try Staxel!",
            "Also try Stardew Valley!",
            "Also try modded Minecraft!",
            "Also try Skyrim!",
            "Also try Age of Empires!",
            "Also try Command & Conquer Generals!",
            "Also try A Hat in Time!",
            "Also try Need for Speed!",
            "Also try Lego Worlds!",
            "Also try Party Hard!",
            "Also try The legend of Zelda!",
            "Also try BlueStacks!",
            "Also try Minecraft (LOL)!",
            "Play with Optifine!",
            "Faithful resource pack!",
            "Keeping you away from low vibrations!",
            "Keeping you away from bad feelings!",
            "Pain is only physical and emotional!",
            "Suffering can't reach the mind!",
            "You're a true creator!",
            "Believe in yourself!",
            "How many splashes exist?",
            "Johann Sebastian Bach!",
            "Tocatta and fugue in D minor!",
            "Working for the light side!",
            "Forgive!",
            "Keeping the knowledge!",
            "Dee D. Jackson - Automatic Lover!",
            "Underpony - Gravity Falls theme (remix)!",
            "Pendulum Distortion - Gravity Falls theme (techno remix)!",
            "Gitta - No more turning back (Kosmonova remix)!",
            "Top Gear Rally (N64) - Coastline!",
            "Sasha - Behind the wheel (electrocash mix)!",
            "Jupiter Prime feat. Lavinia - Secrets of life!",
            "Bubble Toy - This one (Mricky & Danieli mix)!",
            "Bit inversion at bases 2, 4 and 16 = fractals!",
            "Recovery!",
            "Why don't you create your own splashes?",
            "Life is hard, but creating is easier!",
            "Angela Duscio - Monster High fright song!",
            "Boo York, Boo York soundtrack - We are Monster High!",
            "RGB and CMYK!",
            "Pure rainbows!",
            "Using 1.530 pure colors!",
            "You're amazing!",
            "Full polarity!",
            "Maximum power!",
            "100% alcohol free!",
            "100% drugs free!",
            "100% virus free!",
            "100% demon free!",
            "100% evil free!",
            "100% corruption free!",
            "99% lag free!",
            "Money = Temptation!",
            "Money = Opportunity!",
            "Protect your mind!",
            "Protect your feelings!",
            "Protect your body!",
            "Protect yourself!",
            "Protect the others!",
            "Protect the planet!",
            "Protect everything!",
            "You've got the power!",
            "You can guide yourself!",
            "Back to the future!",
            "Time machine!",
            "Doc and Marty!",
            "1.21 GW!",
            "Lightning power!",
            "Never building square houses!",
            "Never building sharp edges!",
            "Building always soft surfaces!",
            "Never compete against others!",
            "You're not your physical body!",
            "The body limits our true powers!",
            "We've got a mission to accomplish!",
            "Watch Oblivion (with Tom Cruise)!",
            "Oblivion tells our old true history!",
            "Solid, triangular or loud U.F.O.s are dangerous!",
            "In the future all machines will be silent!",
            "In the future even plastics will be silent!",
            "In the silence there is the highest harmony!",
            "Almost always ending the phrases with a... !",
            "Infinite calculator!",
            "Infinite energy!",
            "Infinite fractals!",
            "Infinite space!",
            "Infinite time!",
            "Infinite patience!",
            "Infinite pixels!",
            "Infinite power!",
            "Infinite love!",
            "Infinite harmony!",
            "Infinite infinity!",
            "Where there is not light, you can create it!",
            "Where there is not light, light a torch!",
            "Where there is not light, place a torch!",
            "Let there be light!",
            "GNU General Public License v3.0!",
            "Open source!",
            "Distribute it!",
            "Making your day happier!",
            "Wishing you the best!",
            "RH- blood = Ancient genetics!",
            "Music and mathematics!",
            "Everything is made out of mathematics!",
            "Precise vibrations can change the matter!",
            "Our memory is erased a few years after being born!",
            "Celestial!",
            "What if...",
            "We need assistant robots... again!",
            "Our world is too loud!",
            "We need silence to meditate!",
            "We need silence to think!",
            "Water can clean our feelings!",
            "Even offline works fine!",
            "Publicity usually try to brainwash you!",
            "Publicity might be banned in the future!",
            "Only with good publicity!",
            "Try to remember your dreams!",
            "Your dreams give you knowledge!",
            "Trying to improve!",
            "Balnibarbi!",
            "Luggnagg!",
            "Glubbdubdrib!",
            "Japan!",
            "Lagado!",
            "The future can be predicted!",
            "Predicting the future!",
            "The future is in the past!",
            "In the past things were better!",
            "In the past things were more advanced!",
            "Made with the best intention!",
            "TV is too violent nowadays!",
            "Why humanity always destroys itself?",
            "Can we finally live in peace forever?",
            "Always sending smiling emojis!",
            "Before saying anything bad, don't say a thing!",
            "Steampunk!",
            "Different races, from different planets!",
            "Different races, a single humanity!",
            "Wisdom like fortune cookies!",
            "Thanks for coming by!",
            "Have we met before?",
            "Leave a like!",
            "Subscribe!",
            "Donate!",
            "Donate... please!",
            "Donations are welcome!",
            "Donations are appreciated!",
            "Collaboration is welcome!",
            "Water and redstone = frustration!",
            "Have you tried to compose music?",
            "Have you tried to program?",
            "Creeping Creepers!",
            "Cassiopeia!",
            "Constellations!",
            "Just kidding!",
            "Legal in this galaxy!",
            "Triangular!",
            "Pentagonal!",
            "Hexagonal!",
            "Doesn't use any square!",
            "Squares are forbidden!",
            "Forgetting the bad things!",
            "Remembering the good things!",
            "With Dolphin you can play Wii games on PC!",
            "Select well what you read!",
            "Select well what you watch!",
            "Select well what you listen!",
            "Select well what you play!",
            "Select well what you do!",
            "Select well what you eat!",
            "Select well what you think!",
            "Select well what you dream!",
            "Always filter the highest quality!",
            "Without a dark side!",
            "A telescope in the palm of your hand!",
            "A microscope in the palm of your hand!",
            "An infiniscope in the palm of your hand!",
            "4th dimensional!",
            "5th dimensional!",
            "Now I'm gonna guess your thoughts...",
            "Torch spamming!",
            "!txet derorriM",
            "Rocket propelled!",
            "Nether stars!",
            "Beaconator!",
            "You can sleep during thunderstorms!",
            "If your elytra brake you won't loose them!",
            "Breathing on magma blocks!",
            "Breathing inside doors!",
            "Tridents!",
            "Hy-Brasil!",
            "FL Studio has a free, full working demo!",
            "Anti-nuclear!",
            "Nuclear proof!",
            "Cleaning radiations!",
            "Project ozone!",
            "Sky factory!",
            "Joke on how to make a splash: jump into water!",
            "Joke on how to make a splash: throw a potion!",
            "MessageBox.Show(\"Hello world!\");",
            "Walk a bit everyday!",
            "In the source the splashes make sense!",
            "Splashes with encrypted messages!",
            "Well played!",
            "Good gaming!",
            "Let's play!",
            "Story mode!",
            "Top commenter: swagmiter!",
            "Top collaborator: Alexander!",
            "Top donator: Alexander!",
            "Crystal gems!",
            "Mystery shack!",
            "Resurrection!",
            "Life in the Woods: Renaissance!",
            "Curse (launcher)!",
            "Splash 403: forbidden!",
            "Splash 404: not found!",
            "Red!",
            "Orange!",
            "Brown!",
            "Yellow!",
            "Lime!",
            "Green!",
            "Cyan!",
            "Light blue!",
            "Blue!",
            "Purple!",
            "Magenta!",
            "Pink!",
            "Black!",
            "Gray!",
            "Light gray!",
            "White!",
            "Horizon for Xbox 360!",
            "Morse code compatible!",
            ".... . .-.. .-.. --- -.-.--",
            "... --- ...!",
            "65.536 characters!",
            "No internet at home...",
            "Telepathy!",
            "Inspiring!",
            "Keep calm and play Minecraft!",
            "Good night... ZZZZzzz...",
            "Try to sleep 8 hours a day!",
            "Don't care about fame!",
            "Don't care about money!",
            "Just care about perfection!",
            "Made of dreams!",
            "Made of rainbows!",
            "Made of faith!",
            "Made of hope!",
            "Made of illusion!",
            "Breathing!",
            "Subtitles!",
            "Unisex!",
            "Infinity ingots!",
            "Singularities!",
            "ME system active!",
            "Controlled with hands!",
            "Controlled with voice!",
            "Controlled with thought!",
            "Always polite!",
            "Endless splashes!",
            "Happy memories!",
            "Generating memories in real time!",
            "Generating our own universes!",
            "Sounds familiar!",
            "Inspiring music!",
            "It's kinda magic!",
            "Thank you for the music!",
            "Musical creepers!",
            "Harmony can cure any sickness!",
            "Don't you miss the old splashes?",
            "Only high quality splashes!",
            "Only positive splashes!",
            "Harmony exchange!",
            "Cosmic visions!",
            "Returning to paradise!",
            "Nostradamus!",
            "With your help this world is a better place!",
            "The real Laputa is like Gulliver's Travels, not like the film!",
            "Laputa never had a huge tree in the middle!",
            "Only make perfection and someday the world will recognize it!",
            "Fighting weakness with harmony!",
            "White and gray splashes!",
            "Pyramidal!",
            "Red lightnings!",
            "Solving mysteries!",
            "The time traveler pig!",
            "Have you played Minecraft 2.0?",
            "Google how to play Minecraft Indev!",
            "Google \"Minecraft Indev (please open Matts ReadMe.txt!)\"!",
            "Use a proxy to play Minecraft Indev!",
            "Use a proxy to play Minecraft InfDev!",
            "Use Fiddler to play Minecraft Indev!",
            "Use Fiddler to play Minecraft InfDev!",
            "Have you seen the InfDev brick pyramids?",
            "I have a video about the brick pyramids!",
            "Have you seen the InfDev obsidian walls?",
            "Have you played with Rana?",
            "Have you played with Steves?",
            "April Fools!",
            "Etho Slab!",
            "Full of secrets!",
            "Full of friends!",
            "Berrerabili!",
            "Berrerabilgarria!",
            "See you soon!",
            "Minis mania!",
            "You're not alone!",
            "Furnace minecarts!",
            "Dipper Pines!",
            "Mabel Pines!",
            "Grunkle Stan!",
            "Soos!",
            "Wendy!",
            "Waddles!",
            "Dungeons, dungeons and more dungeons!",
            "Happy birthday!",
            "Helping the community!",
            "Together we're stronger!",
            "Snowing at height 512?!",
            "Bienvenido!",
            "Welcome!",
            "Don't give up!",
            "Listen to classical music!",
            "Redstoning!",
            "Have you tried the secrets!",
            "Now with secret creepers!",
            "Finally with Rana!",
            "Now with more tools!",
            "With more tools than you might think!",
            "MC 1.13+ to 1.12.2- compatible!",
            "Chain reaction!",
            "Pink Withers!",
            "Place your text here!",
            "Take care of your pets!",
            "Inspired and inspiring!",
            Math.PI.ToString().Replace('.', ',') + "!",
            Math.E.ToString().Replace('.', ',') + "!",
            "Now with upside down worlds!",
            "Now with self-destruct worlds!",
            "Random biomes!",
            "Random lighting!",
            "The first with 1.13+ support!",
            "Spoilers!",
            "Random splashes give you messages!",
            "With fractal encryption!",
            "The work of millions!",
            "Draconic power!",
            "Hola, bienvenido!",
            "The MC wiki has some incorrect info!",
            "Now with custom resource packs!",
            "Now with Monster High bios!",
            "The first with 1.14+ support!",
            "Now with night vision!",
            "Now with real time vision!",
            "Now with a 3D block simulator!",
            "Now with more universal tools!",
            "Intel HD Graphics crashes with VBOs!",
            "Optifine can turn off the VBOs!",
            "Thank you Optifine!",
            "Try Raven Pack for Twitch by ISpectre23!",
            "Now with secret knowledge tools!",
            "1st dimension fractal: diagonal line!",
            "2nd dimension fractal: diagonal rhombus!",
            "3rd dimension fractal: perfect circle!",
            "4th dimension fractal: 16x 3rd dimension?!",
            "5th dimension fractal: unknown?!",
            "Fractals with the previous dimensions!",
            "Seed \"Draculaura\" for MC 1.13!",
            "Seed -1994576438 for MC 1.13!",
            "Now with resource pack converter!",
            "Click on the lightning to start a tool!",
            "Minecraft Earth!",
            "Now with audio recording in real time!",
            "Now with lissajous curves!",
            "Now with a real time FFT!",
            "Now with a score viewer!",
        });

        /// <summary>
        /// The original splashes from Minecraft InfDev, from the class "q.java".
        /// </summary>
        internal static readonly string[] i = new string[]
        {
            "Pre-beta!", "As seen on TV!", "Awesome!", "100% pure!", "May contain nuts!", "Better than Prey!", "More polygons!", "Sexy!", "Limited edition!", "Flashing letters!", 
            "Made by Notch!", "Coming soon!", "Best in class!", "When it's finished!", "Absolutely dragon free!", "Excitement!", "More than 5000 sold!", "One of a kind!", "700+ hits on YouTube!", "Indev!", 
            "Spiders everywhere!", "Check it out!", "Holy cow, man!", "It's a game!", "Made in Sweden!", "Uses LWJGL!", "Reticulating splines!", "Minecraft!", "Yaaay!", "Alpha version!", 
            "Singleplayer!", "Keyboard compatible!", "Undocumented!", "Ingots!", "Exploding creepers!", "That's not a moon!", "l33t!", "Create!", "Survive!", "Dungeon!", 
            "Exclusive!", "The bee's knees!", "Down with O.P.P.!", "Closed source!", "Classy!", "Wow!", "Not on steam!", "9.95 euro!", "Half price!", "Oh man!", 
            "Check it out!", "Awesome community!", "Pixels!", "Teetsuuuuoooo!", "Kaaneeeedaaaa!", "Now with difficulty!", "Enhanced!", "90% bug free!", "Pretty!", "12 herbs and spices!", 
            "Fat free!", "Absolutely no memes!", "Free dental!", "Ask your doctor!", "Minors welcome!", "Cloud computing!", "Legal in Finland!", "Hard to label!", "Technically good!", "Bringing home the bacon!", 
            "Indie!", "GOTY!", "Ceci n'est pas une title screen!", "Euclidian!", "Now in 3D!", "Inspirational!", "Herregud!", "Complex cellular automata!", "Yes, sir!", "Played by cowboys!", 
            "OpenGL 1.1!", "Thousands of colors!", "Try it!", "Age of Wonders is better!", "Try the mushroom stew!", "Sensational!", "Hot tamale, hot hot tamale!", "Play him off, keyboard cat!", "Guaranteed!", "Macroscopic!", 
            "Bring it on!", "Random splash!", "Call your mother!", "Monster infighting!", "Loved by millions!", "Ultimate edition!", "Freaky!", "You've got a brand new key!", "Water proof!", "Uninflammable!", 
            "Whoa, dude!", "All inclusive!", "Tell your friends!", "NP is not in P!", "Notch <3 Ez!", "Music by C418!"
        };
        // "OW KNOWS!", throw new IllegalStateException("OW KNOWS!"); // Again from InfDev, package net.minecraft.a.a.a.

        internal static readonly int Índice_Hermitcraft = Lista_Líneas.IndexOf("Hermitcraft!");
        internal static readonly int Índice_Monster_High = Lista_Líneas.IndexOf("Monster High!");
        internal static readonly int Índice_Jupisoft = Lista_Líneas.IndexOf("Made by Jupisoft!");

        /// <summary>
        /// Function that looks for repeated splash texts, for a quick editing and fixing.
        /// Note the only official repeated splash is "Déjà vu!".
        /// </summary>
        internal static void Buscar_Splashes_Repetidos()
        {
            SortedDictionary<string, string> Diccionario_ASCII = new SortedDictionary<string, string>();
            SortedDictionary<string, object> Diccionario_Únicos = new SortedDictionary<string, object>();
            SortedDictionary<string, int> Diccionario_Repeticiones = new SortedDictionary<string, int>();
            foreach (string Línea in Lista_Líneas)
            {
                string Texto_Caracteres = null;
                foreach (char Caracter in Línea)
                {
                    int Valor_Caracter = "\u00c0\u00c1\u00c2\u00c8\u00ca\u00cb\u00cd\u00d3\u00d4\u00d5\u00da\u00df\u00e3\u00f5\u011f\u0130\u0131\u0152\u0153\u015e\u015f\u0174\u0175\u017e\u0207\u0000\u0000\u0000\u0000\u0000\u0000\u0000 !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~\u0000\u00c7\u00fc\u00e9\u00e2\u00e4\u00e0\u00e5\u00e7\u00ea\u00eb\u00e8\u00ef\u00ee\u00ec\u00c4\u00c5\u00c9\u00e6\u00c6\u00f4\u00f6\u00f2\u00fb\u00f9\u00ff\u00d6\u00dc\u00f8\u00a3\u00d8\u00d7\u0192\u00e1\u00ed\u00f3\u00fa\u00f1\u00d1\u00aa\u00ba\u00bf\u00ae\u00ac\u00bd\u00bc\u00a1\u00ab\u00bb\u2591\u2592\u2593\u2502\u2524\u2561\u2562\u2556\u2555\u2563\u2551\u2557\u255d\u255c\u255b\u2510\u2514\u2534\u252c\u251c\u2500\u253c\u255e\u255f\u255a\u2554\u2569\u2566\u2560\u2550\u256c\u2567\u2568\u2564\u2565\u2559\u2558\u2552\u2553\u256b\u256a\u2518\u250c\u2588\u2584\u258c\u2590\u2580\u03b1\u03b2\u0393\u03c0\u03a3\u03c3\u03bc\u03c4\u03a6\u0398\u03a9\u03b4\u221e\u2205\u2208\u2229\u2261\u00b1\u2265\u2264\u2320\u2321\u00f7\u2248\u00b0\u2219\u00b7\u221a\u207f\u00b2\u25a0\u0000".IndexOf(Caracter);
                    if (Valor_Caracter <= -1) Texto_Caracteres += Caracter;
                }
                if (!string.IsNullOrEmpty(Texto_Caracteres))
                {
                    Diccionario_ASCII.Add(Línea, Texto_Caracteres); // Look for non-standard characters.
                }
                if (!Diccionario_Únicos.ContainsKey(Línea))
                {
                    Diccionario_Únicos.Add(Línea, null);
                }
                else
                {
                    if (!Diccionario_Repeticiones.ContainsKey(Línea))
                    {
                        Diccionario_Repeticiones.Add(Línea, 2);
                    }
                    else
                    {
                        Diccionario_Repeticiones[Línea]++;
                    }
                }
            }
            foreach (KeyValuePair<string, string> Entrada in Diccionario_ASCII)
            {
                System.Windows.Forms.Clipboard.SetText(Entrada.Key); // Copy the text for quick searching below.
                System.Windows.Forms.MessageBox.Show(Entrada.Key, Entrada.Value);
            }
            foreach (KeyValuePair<string, int> Entrada in Diccionario_Repeticiones)
            {
                System.Windows.Forms.Clipboard.SetText(Entrada.Key); // Copy the text for quick searching below.
                System.Windows.Forms.MessageBox.Show(Entrada.Key, Entrada.Value.ToString()); // Show how many repetitions it has.
            }
        }

        // Looking for the InfDev brick pyramid code... is this?

        /*package net.minecraft.a.a;

        import java.util.*;
        import net.minecraft.a.a.b.ad;
        import net.minecraft.a.a.b.af;
        import net.minecraft.a.a.b.x;
        import net.minecraft.a.a.c.a.c;

        // Referenced classes of package net.minecraft.a.a:
        //            c, g

        public class f
        {

            public int a(int i, int j, int k)
            {
                if(j < 0)
                    return x.q.ao;
                if(j >= 128)
                    return 0;
                else
                    return a(i >>> 4, k >>> 4).a(i & 0xf, j, k & 0xf);
            }

            public boolean a(int i, int j, int k, int l)
            {
                if(j < 0)
                    return false;
                if(j >= 128)
                    return false;
                net.minecraft.a.a.c c1 = a(i >>> 4, k >>> 4);
                i &= 0xf;
                k &= 0xf;
                if((c1.a(i, j, k) & 0xff) == l)
                {
                    return false;
                } else
                {
                    c1.a(i, j, k, l);
                    return true;
                }
            }

            public f()
            {
                a = new Random();
                b = new c(16);
                c = new c(16);
                d = new c(8);
                e = new c(4);
                f = new c(4);
                g = new c(5);
                new c(3);
                new c(3);
                new c(3);
                h = ((Map) (new HashMap()));
            }

            private net.minecraft.a.a.c a(int i, int j)
            {
                i = ((int) (new g(this, i, j)));
                if((j = ((int) ((net.minecraft.a.a.c)h.get(((Object) (i)))))) == null)
                {
                    j = ((int) (new net.minecraft.a.a.c(this, a(((g) (i))))));
                    h.put(((Object) (i)), ((Object) (j)));
                }
                return ((net.minecraft.a.a.c) (j));
            }

            public byte[] a(g g1)
            {
                byte abyte0[] = new byte[32768];
                int i = g1.a << 4;
                g1 = ((g) (g1.b << 4));
                int j = 0;
                for(int k = i; k < i + 16; k++)
                {
                    for(int l = ((int) (g1)); l < g1 + 16; l++)
                    {
                        int i1 = k / 1024;
                        int j1 = l / 1024;
                        float f1 = (float)(b.a((float)k / 0.03125F, 0.0D, (float)l / 0.03125F) - c.a((float)k / 0.015625F, 0.0D, (float)l / 0.015625F)) / 512F / 4F;
                        float f2 = (float)f.a((float)k / 4F, (float)l / 4F);
                        float f3 = (float)g.a((float)k / 8F, (float)l / 8F) / 8F;
                        f2 = f2 <= 0.0F ? (float)(e.a((float)k * 0.2571428F, (float)l * 0.2571428F) * (double)f3) : (float)((d.a((float)k * 0.2571428F * 2.0F, (float)l * 0.2571428F * 2.0F) * (double)f3) / 4D);
                        f1 = ((float) ((int)(f1 + 64F + f2)));
                        if((float)f.a(k, l) < 0.0F)
                        {
                            f1 = ((float) (f1 / 2 << 1));
                            if((float)f.a(k / 5, l / 5) < 0.0F)
                                f1++;
                        }
                        for(int k1 = 0; k1 < 128; k1++)
                        {
                            int l1 = 0;
                            if((k == 0 || l == 0) && k1 <= f1 + 2)
                                l1 = x.ac.ao;
                            else
                            if(k1 == f1 + 1 && f1 >= 64 && Math.random() < 0.02D)
                                l1 = x.Q.ao;
                            else
                            if(k1 == f1 && f1 >= 64)
                                l1 = x.h.ao;
                            else
                            if(k1 <= f1 - 2)
                                l1 = x.g.ao;
                            else
                            if(k1 <= f1)
                                l1 = x.i.ao;
                            else
                            if(k1 <= 64)
                                l1 = x.o.ao;
                            a.setSeed(i1 + j1 * 13871);
                            int i2 = (i1 << 10) + 128 + a.nextInt(512);
                            int j2 = (j1 << 10) + 128 + a.nextInt(512);
                            i2 = k - i2;
                            j2 = l - j2;
                            if(i2 < 0)
                                i2 = -i2;
                            if(j2 < 0)
                                j2 = -j2;
                            if(j2 > i2)
                                i2 = j2;
                            if((i2 = 127 - i2) == 255)
                                i2 = 1;
                            if(i2 < f1)
                                i2 = ((int) (f1));
                            if(k1 <= i2 && (l1 == 0 || l1 == x.o.ao))
                                l1 = x.Y.ao;
                            if(l1 < 0)
                                l1 = 0;
                            abyte0[j++] = (byte)l1;
                        }

                    }

                }

                return abyte0;
            }

            private Random a;
            private c b;
            private c c;
            private c d;
            private c e;
            private c f;
            private c g;
            private Map h;
        }*/
    }
}
