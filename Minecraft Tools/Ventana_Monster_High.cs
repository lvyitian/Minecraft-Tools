using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Monster_High : Form
    {
        public Ventana_Monster_High()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Structure that holds up all the information about a Monster High character.
        /// 
        /// [RTF format info]:
        /// 
        /// "\b ": bold start.
        /// "\b0 ": bold end.
        /// 
        /// "\i ": italic start.
        /// "\i0 ": italic end.
        /// 
        /// "\ul ": underline start.
        /// "\ulnone ": underline end.
        /// 
        /// "\strike ": strike start.
        /// "\strike0 ": strike end.
        /// 
        /// "\sub ": subtext start.
        /// "\nosupersub ": subtext end.
        /// 
        /// "\super ": supertext start.
        /// "\nosupersub ": supertext end.
        /// 
        /// "\highlight1 ": highlight start.
        /// "\highlight0 ": highlight end.
        /// 
        /// "\cf1 ": colored font.
        /// "\cf0 ": default colored font.
        /// 
        /// "\pard\qc ": center text.
        /// "\pard ": left text.
        /// "\pard\qr ": right text.
        /// "\pard\qj ": justified text.
        /// 
        /// "\fs18 ": font size 9.
        /// "\fs20 ": font size 10.
        /// "\fs22 ": font size 11.
        /// "\fs24 ": font size 12.
        /// "\fs160 ": font size 80.
        /// Note: the font size seems to be the double on the source code than the actual value.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Personajes
        {
            /// <summary>
            /// The resource name of a Monter High character.
            /// </summary>
            internal string Resource;
            /// <summary>
            /// The name of a Monter High character.
            /// </summary>
            internal string Name;
            /// <summary>
            /// The family of a Monter High character.
            /// </summary>
            internal string Family;
            /// <summary>
            /// The age of a Monter High character.
            /// </summary>
            internal string Age;
            /// <summary>
            /// The description of a Monter High character.
            /// </summary>
            internal string Description;
            /// <summary>
            /// The favorite food of a Monter High character.
            /// </summary>
            internal string Favorite_Food;
            /// <summary>
            /// The favorite activity of a Monter High character.
            /// </summary>
            internal string Favorite_Activity;
            /// <summary>
            /// The killer style of a Monter High character.
            /// </summary>
            internal string Killer_Style;
            /// <summary>
            /// The pet of a Monter High character.
            /// </summary>
            internal string Pet;
            /// <summary>
            /// The pet peeve of a Monter High character.
            /// </summary>
            internal string Pet_Peeve;
            /// <summary>
            /// The monster quirk of a Monter High character.
            /// </summary>
            internal string Monster_Quirk;
            /// <summary>
            /// The friends of a Monter High character.
            /// </summary>
            internal string Friends;

            /// <summary>
            /// The matrix that contains a list of all the Monster High characters.
            /// </summary>
            internal Personajes(string Resource, string Name, string Parent, string Age, string Description, string Favorite_Food, string Favorite_Activity, string Killer_Style, string Pet, string Pet_Peeve, string Monster_Quirk, string Friends)
            {
                this.Resource = Resource;
                this.Name = Name;
                this.Family = Parent;
                this.Age = Age;
                this.Description = Description;
                this.Favorite_Food = Favorite_Food;
                this.Favorite_Activity = Favorite_Activity;
                this.Killer_Style = Killer_Style;
                this.Pet = Pet;
                this.Pet_Peeve = Pet_Peeve;
                this.Monster_Quirk = Monster_Quirk;
                this.Friends = Friends;
            }

            internal static readonly Personajes[] Matriz_Personajes = new Personajes[]
            {
                new Personajes
                (
                    "Abbey", // Resource.
                    "Abbey Bominable", // Name.
                    "Daughter of The Yeti", // Family.
                    "16", // Age.
                    "I try to not make icy first impressions…but can’t help being honest. Don’t understand the games ghouls play, like why I’m not to like Heath Burns. He is funny. I like to laugh. Done", // Description.
                    "The cheese of yak and pancakes", // Favorite food.
                    "Boarding on the snow is maxed out totally to the awesome", // Favorite activity.
                    "Fur. It is practical for the blending in and the showing off. My special ice crystal I also wear for the perpetuating of bodily coldness of my home", // Killer style.
                    "Shiver™ is my wooly mammoth. Her feelings being the kind not so easily punctured", // Pet.
                    "The ritual of the dating. I do not understand the trapping of the boy with the bait of flirtation…it seems to me not honorable", // Pet peeve.
                    "I am lacking in the tact. At altitude, talking is an oxygen waste, so words are pointed. I am often puncturing the feelings here down below. I am not wishing to fill others with the sadness but some times I do. Also, I am cold, both in the touching and being touched", // Monster quirk.
                    "Deuce Gorgon, Ghoulia Yelps, Frankie Stein and Draculaura" // Friends.
                ),
                new Personajes
                (
                    "Aery", // Resource.
                    "Aery Evenfall", // Name.
                    "", // Family.
                    "", // Age.
                    "Dream Monster: A skeleton. The darkest nights bring out the brightest stars, and there’s nothing better than to watch the twinkling herd galloping across a midnight sky", // Description.
                    "", // Favorite food.
                    "I love listening to my beasties hopes, dreams, and even fears. I try to give them stellar advice, but I know sometimes they just want someone to hear them", // Favorite activity.
                    "I like to call my style goth-mystic: purples and blacks, with flames, bone, and celestial elements", // Killer style.
                    "", // Pet.
                    "When monsters accuse me of eavesdropping on their convos because I sometimes know their secrets", // Pet peeve.
                    "I have a special knack for knowing what my beasties are thinking before they say it. I wouldn’t call it a flaw, as much as a gift", // Monster quirk.
                    "Pyxis Prepstockings, Frets Quartzmane and Bay Tidechaser" // Friends.
                ),
                new Personajes
                (
                    "Amanita", // Resource.
                    "Amanita Nightshade", // Name.
                    "", // Family.
                    "17 in Corpse Flower years", // Age.
                    "Bad seed of the Corpse Flower", // Description.
                    "Filtered spring water with a spritz of organic fertilizer", // Favorite food.
                    "I love being adored and celebrated for my inherent beauty – both inside and out", // Favorite activity.
                    "My carefully cultivated look makes every other flower in the garden of style look like a weed by comparison. It isn’t as if they are lacking in color, but their arrangement appears merely budding when planted next to my full bloom of fashion", // Killer style.
                    "I have an adoring maggot that is die-rling as both pet and accessory", // Pet.
                    "You want to know what rots my roots? Bad service and the thin-skinned monsters who provide it. If you can’t get it right, then don’t lose your leaves when I express dissatisfaction", // Pet peeve.
                    "I believe there’s nothing so wonderful as the sound of my own melodic voice, but there are many who feel that perhaps their voices are equally important. I have spent many years pondering this and yet still see little merit in that argument", // Monster quirk.
                    "" // Friends.
                ),
                new Personajes
                (
                    "Astranova", // Resource.
                    "Astranova", // Name.
                    "Daughter of the “Comet Aliens”", // Family.
                    "15 in space years", // Age.
                    "According to my new earthmonsterling friends, my Comet Alien scaritage gives “out-of-this-world” a whole new meaning! It’s kinda freaky being lightyears from home, but I’m having a stellar time adapting to my new surroundings", // Description.
                    "Spaghetti and moonballs (Gran’s secret recipe)", // Favorite food.
                    "Singing. Music is common ground – and space. It is the language understood by all monsters", // Favorite activity.
                    "I love glam rock. The more sparkle and out-of-this-world shine the better. My quest is to be the most dazzling object in a universe full of stars!", // Killer style.
                    "No creature hibernates for as long as I’ve been in stasis, so it would not be fair to have a pet", // Pet.
                    "When monsters ask me what planet I’m from. I’m a comet ghoul – the entire galaxy is my home", // Pet peeve.
                    "After traveling for lightyears, sometimes I have difficulty navigating Monster High. What I’m trying to say gets lost in translation, or I don’t get my new ghoulfriends’ fangisms, and then I feel like our conversations just orbit ‘round and ‘round", // Monster quirk.
                    "Elle Eedee, Luna Mothews and Mouscedes King" // Friends.
                ),
                new Personajes
                (
                    "Avea", // Resource.
                    "Avea Trotter", // Name.
                    "Daughter of a centaur (father) and a harpy (mother)", // Family.
                    "17", // Age.
                    "I’m a scary-stylish hybrid monster, who’s part centaur, part harpy. I transferred to Monster High with my three hybrid beasties Bonita Femur, Neighthan Rot and Sirena Von Boo. While I know I can be stubborn, a nice, long gallop always helps me rein in my opinions", // Description.
                    "Corn, oats, alfalfa and wheat grass smoothies. I’m also pretty fond of apples and sugar cubes", // Favorite food.
                    "I can run like the wind, and there’s nothing I like better than to go galloping off across, well, anywhere", // Favorite activity.
                    "I love equestrian haunt couture and there’s nothing that makes me want to kick up my heels like a new pair of gloves", // Killer style.
                    "I’ve never found one that has been able to keep pace with me", // Pet.
                    "I’m not a pony ride at a carnival so no, I won’t give you a lift, let you “hitch” a ride or just “trot you over to your next class.” It’s undignified", // Pet peeve.
                    "I’m a little bit…okay a lot…stubborn. Once I’ve made up my mind, I hate to be reined in, and I have a hard time not bucking the trend", // Monster quirk.
                    "Sirena Von Boo, Neighthan Rot and Bonita Femur" // Friends.
                ),
                new Personajes
                (
                    "Batsy", // Resource.
                    "Batsy Claro", // Name.
                    "Daughter of the White Vampire Bat", // Family.
                    "17", // Age.
                    "I’m a nature-loving bat who's fiercely passionate about protecting the environment. Rescuing an endangered plant may be what brought me to Monster High, but my mission to teach monsters how to care for the planet is what will keep me rooted here", // Description.
                    "Blood...oranges", // Favorite food.
                    "Introducing monsters to the jungle for the first time. Seeing their faces fright up at the unnatural wonder of it all never gets old", // Favorite activity.
                    "I take my fashion inspiration from the jungle around me, like the green of woven palm fronds and the fright colors of orchids. They help me blend in and stand out all at the same time", // Killer style.
                    "Pet? Perish the thought", // Pet.
                    "When monsters want to take flora or fauna from the jungle back home with them. I understand why they would wish to do so, but the best place for any plant or creature is its home", // Pet peeve.
                    "I have a real hang-up with monsters who choose to stay in the dark when it comes to protecting the world where we unlive. This is the only world we have, and we shouldn’t be turning a blind eye when it comes to taking care of it", // Monster quirk.
                    "Venus McFlytrap and Jane Boolittle" // Friends.
                ),
                new Personajes
                (
                    "Bay", // Resource.
                    "Bay Tidechaser", // Name.
                    "", // Family.
                    "", // Age.
                    "Dream Monster: A sea monster. I unlive for the ocean wind in my mane and the waves on my hoofs. I mean is there anything better than a gallop on the beach at dawn?", // Description.
                    "", // Favorite food.
                    "Hang tail! I’m a total surfer ghoul. I love to chase the waves – it’s like another way to fly!", // Favorite activity.
                    "Boho surfer chic. I love frothy fabrics floating around me and ghoulery that shows off the natural beauty of the sea", // Killer style.
                    "", // Pet.
                    "When other ghouls think I’m flighty just because I have a casual unlifestyle", // Pet peeve.
                    "I’m graceful on the beach, and fluid on the waves, but when it comes to the dance floor, I have 4 left feet", // Monster quirk.
                    "Pyxis Prepstockings, Frets Quartzmane and Aery Evenfall" // Friends.
                ),
                new Personajes
                (
                    "Bonita", // Resource.
                    "Bonita Femur", // Name.
                    "Daughter of a Moth (father) and a skeleton (mother)", // Family.
                    "16", // Age.
                    "I’m a part moth, part skeleton monster hybrid who transferred to Monster High with my beasties Neighthan Rot, Sirena Von Boo and Avea Trotter. I love freaky fab vintage fashions, but because I get nervous sometimes, I tend to chew on them—especially if they are wool or silk", // Description.
                    "Wool and silk", // Favorite food.
                    "Shopping. A ghoul like me can never get tired of getting new clothes, especially when all my clothes have chew marks and holes", // Favorite activity.
                    "I’m totally attracted to bright colors and my favorite place to shop for them are flea markets! Don’t judge. You can always find freaky cool vintage fashions and make them your own", // Killer style.
                    "I think I might be too nervous to have a pet. I would always be worried about how it was doing when I was away from it at school", // Pet.
                    "When people call me an airhead, because they always see me fluttering down the halls. Not really a fan of nets either", // Pet peeve.
                    "When I get nervous, I tend to chew on my clothes. I know it’s a bad habit but a nice wool blend really calms me down", // Monster quirk.
                    "Neighthan Rot, Avea Trotter and Sirena Von Boo" // Friends.
                ),
                new Personajes
                (
                    "Casta", // Resource.
                    "Casta Fierce", // Name.
                    "Daughter of Circe", // Family.
                    "19", // Age.
                    "Even though I only perform live once a year on Halloween, I’m still one of the biggest stars in the monster world. My band The Spells and I played Monster High on Halloween 2014, and thankfully I got all my lyrics right so I didn’t turn anyone in the audience into…something else", // Description.
                    "Tomatoes and monsterella cheese with balsamic vinegrrr", // Favorite food.
                    "I’m a performer. I love being on the stage under the lights with an audience in front of me. There’s nothing like it, and it’s all I ever want to do", // Favorite activity.
                    "I like lots of buckles and straps crisscrossed over black and purple fashions splashed with glitter", // Killer style.
                    "I can’t have a pet because I don’t think I could stand being constantly asked, ''Oh how cute…was this a fan of yours?", // Pet.
                    "''Fans'' who show up late and leave early. If you’re coming to my show, please be on time and stay till the end. It’s a distraction, especially to the real fans that really want to be there. If that makes me sound like a diva, I can unlive with that!", // Pet peeve.
                    "I have to make sure to word the lyrics of my songs in just the right way or I run the risk of turning my audience into…well…let’s just say frogs don’t usually ask for an encore", // Monster quirk.
                    "Catty Noir and Operetta" // Friends.
                ),
                new Personajes
                (
                    "Catrine", // Resource.
                    "Catrine Demew", // Name.
                    "Daughter of a Werecat", // Family.
                    "415", // Age.
                    "I grew up on the streets of Scaris, where I sketched purrrfect portraits of tourists. I came to Monster High to continue my education and work on becoming a more clawesome artist", // Description.
                    "Mille-feuille. It is three layers of the puff pastry filled with the sweet scream and the sugar powdered on top. Ah…c’est magnifique!", // Favorite food.
                    "I love to organize my paints, brushes, chalks, inks, pens and colored pencils. I find the process…calming", // Favorite activity.
                    "While I prefurrr that my art is what attracts the monster tourist, I find that wearing the just right Scarisian fashion helps frame my clawesome artistic abilities", // Killer style.
                    "I do not have a pet, but I love the pigeons of Scaris. They are très doux", // Pet.
                    "Monsters that won’t sit still while I am attempting to sketch them. It is very annoying", // Pet peeve.
                    "I am a purrrfectionist, which makes it très difficile sometimes to finish the sketch. Even when the tourist loves the portrait, I am still wanting to run after them and ask for more time to make it purrfect", // Monster quirk.
                    "Abbey Bominable, Deuce Gorgon, Cleo De Nile, Clawdeen Wolf, Clawd Wolf and Catty Noir" // Friends.
                ),
                new Personajes
                (
                    "Catty", // Resource.
                    "Catty Noir", // Name.
                    "Daughter of The Werecats!", // Family.
                    "16", // Age.
                    "I always wondered what it would be like to put my pop music career on hold and go to school like a normal teenager. So now I’m a member of the Monster High student bodies. I may go on the road some times, but when I’m not performing, you can find me haunting the MH howlways, trying to blend in", // Description.
                    "Chilling cheese fries. They are my favorite after-concert food", // Favorite food.
                    "I like personally answering fan letters, really I do. I know that may not sound very glamorous, but it helps me feel connected to them, especially because without their support I wouldn’t be here", // Favorite activity.
                    "When I’m performing, I love big, flashy, larger-than-unlife outfits because they are ghoulishly glitzy, creeporifically cool and fangsolutely fun! Even when I’m offstage, I like to wear fashions that sparkle and flash ‘cause they make me feel lucky", // Killer style.
                    "Not having a pet is just one of the sacrifices I felt I had to make to pursue being a singer, but now I’m looking forward to getting something creepy cute and scary sweet", // Pet.
                    "Not being able to make my own schedule. After years of having every minute of my unlife planned for me, I am OH-VER-IT!", // Pet peeve.
                    "I’m really superstitious. For instance, I always eat the same thing two hours before every concert: 7 chicken nuggets, 5 apple slices, 1 strawscarry shake. I have to enter stage left under one ladder and exit stage right under another, and finally, I always wear a piece of broken mirror when I’m on stage. I find it very unlucky if any of these things don’t happen", // Monster quirk.
                    "Abbey Bominable, Cleo De Nile, Clawdeen Wolf, Clawd Wolf and Catrine Demew" // Friends.
                ),
                new Personajes
                (
                    "Clawd", // Resource.
                    "Clawd Wolf", // Name.
                    "Son of the Werewolf", // Family.
                    "17", // Age.
                    "I love football and casketball, but you can’t be a fierce superstar athlete forever, so I’m also scary focused on my studies. I’m furrriously busy playing sports and studying, but I always have time for my ghoul Draculaura", // Description.
                    "Steak…and lots of it", // Favorite food.
                    "I love football. I love the training, the strategy, the competition, and the atmosphere on game day. It's the perfect sport", // Favorite activity.
                    "Casual à la Clawdeen…it would be dumb for me to not listen to fashion advice from her", // Killer style.
                    "A gargoyle bulldog named Rockseena™. She's my number one rock-solid fan", // Pet.
                    "Monsters who think that casketball and football players are all dumb jocks. I'll compare GPAs with anybody…okay maybe anybody but Ghoulia Yelps™ :)", // Pet peeve.
                    "I shed…a lot. When I get out of the shower, my family becomes the proud owners of a fur-lined tub. I might as well comb my hair with a lint brush and save the extra step", // Monster quirk.
                    "Draculaura, Deuce Gorgon, Holt Hyde and Jackson Jekyll" // Friends.
                ),
                new Personajes
                (
                    "Clawdeen", // Resource.
                    "Clawdeen Wolf", // Name.
                    "Daughter of the Werewolf", // Family.
                    "15", // Age.
                    "I’m a confident, no-nonsense werewolf who screams of becoming a fashion designer. I’m fiercely protective of my friends and family, so don’t even think about messing with my pack, especially my best ghoulfriend Draculaura and my kid sis Howleen", // Description.
                    "Steak…rare", // Favorite food.
                    "Shopping and flirting with boys!", // Favorite activity.
                    "I'm a fierce fashionista with a confident no-nonsense attitude. I'm also gorgeous, intimidating and absolutely loyal to my friends", // Killer style.
                    "My scary-cute little kitten Crescent™ is as fuzzy as I am", // Pet.
                    "I hate having so many of my brothers and sisters in school at the same time. They're annoying, embarrassing and totally know how to push my buttons", // Pet peeve.
                    "My hair is worthy of a shampoo commercial and that's just what grows on my legs. Plucking and shaving is definitely a full-time job, but that's a small price to pay for being scarily fabulous", // Monster quirk.
                    "Draculaura, Frankie Stein, Skelita Calaveras and Jinafire Long" // Friends.
                ),
                new Personajes
                (
                    "Clawdia", // Resource.
                    "Clawdia Wolf", // Name.
                    "Daughter of the Werewolf", // Family.
                    "19", // Age.
                    "Before heading to Hauntlywood to pursue my career as a screamwriter, I studied drama in Londoom. I miss my pack at home, especially Clawd, Clawdeen and Howleen. While I don’t get to visit as often as I’d like, I’m a clawesome pen pal, especially to Howleen", // Description.
                    "I love fangers and mash, roast beast and fish and crypts. I kind of like shepherd’s pie, too…only without the wool", // Favorite food.
                    "Writing! I write every day ‘cause if you don’t write, you can’t call yourself a writer. One day I hope to be a screamwriter in Hauntlywood!", // Favorite activity.
                    "Tribal Prep. I love to mix graphic tribal prints with some scholarly flare and because Londoom is fangsolutely undead with fashion and literary history, both my brain and my wardrobe benefit. To paraphrase Spookspheare, “This is the stuff as screams are made on”", // Killer style.
                    "I’ve been clawing through all my literary references trying to decide what I should get…maybe I’ll choose an albatross or ooh…a raven", // Pet.
                    "Monsters who ask to borrow my pen. Seriously. I have one pen that I use, I’ve had it forever, and I don’t lend it out. It writes perfectly, and I cannot create without it", // Pet peeve.
                    "Whenever I think or get nervous I chew. Pencils, pens, paper clips, gum…the corner of my iCoffin. One year Clawdeen got me a manicure for my birthday but on the way home from the nail salon I got an idea for a story, and well, let’s just say little sis was not happy with what I chewed on that day", // Monster quirk.
                    "Clawd Wolf, Clawdeen Wolf and Howleen Wolf" // Friends.
                ),
                new Personajes
                (
                    "Cleo", // Resource.
                    "Cleo De Nile", // Name.
                    "Daughter of the Mummy", // Family.
                    "5,843 (give or take a few years)", // Age.
                    "As a descendent of Egyptian royalty, I reign over the student bodies of Monster High. I’m captain of the Fear Squad and my boyfriend, Deuce Gorgon, is the fiercest manster in school. My older sister Nefera and my frenemy Toralei bring out my competitive side, but I’m not a diva without feelings. I’m a true friend, too. Just ask my beastie Ghoulia Yelps", // Description.
                    "Grapes, especially when someone is feeding them to me", // Favorite food.
                    "Ruling the halls of Monster High and being captain of the fearleading squad", // Favorite activity.
                    "I'm a true Egyptian princess complete with headdress, exotic jewelry and, oh yeah, the occasional stray bandage wrapping", // Killer style.
                    "Hissette™ is my sweet Egyptian cobra. Her hiss is honestly much worse than her, er, somewhat poisonous bite", // Pet.
                    "When someone refuses to obey me. And, having to deal with my sister Nefera!", // Pet peeve.
                    "The dark. Yes, I'm a monster who's afraid of the dark. Get over it", // Monster quirk.
                    "Deuce Gorgon, Ghoulia Yelps, Clawdeen Wolf and Frankie Stein" // Friends.
                ),
                new Personajes
                (
                    "Draculaura", // Resource.
                    "Draculaura", // Name.
                    "Daughter of Dracula", // Family.
                    "1600", // Age.
                    "I’m not exactly your typical vampire. I love gossip, freaky-fab fashion and fanging out with my friends, especially my boyfriend Clawd Wolf, who happens to be the brother of my beast ghoulfriend Clawdeen Wolf. I’m also a strict vegetarian who faints at the sight of blood", // Description.
                    "I'm a vegan. No icky blood for me, so it's fruits, vegetables and a lot of iron supplements", // Favorite food.
                    "I love smiling, laughing and encouraging my friends", // Favorite activity.
                    "My dad calls me Draculaura, but to my friends I'm Ula D™. I love to splash my black outfits with some cheery pink, and I even carry a frilly umbrella so I can take an occasional walk in the sun", // Killer style.
                    "Count Fabulous™ is my BFF (Bat Friend Forever). He's a very proud and proper bat, but I just can't help but dress him in the cutest little outfits", // Pet.
                    "The lack of vegan selections in the Monster High creepateria is so sad", // Pet peeve.
                    "Since I can't see my reflection in a mirror, I have to leave the house not knowing if my clothes and makeup are just right. Of course, after 1,600 years of practice I've gotten pretty good at it", // Monster quirk.
                    "Clawd Wolf, Frankie Stein and Clawdeen Wolf" // Friends.
                ),
                new Personajes
                (
                    "Elissabat", // Resource.
                    "Elissabat", // Name.
                    "Daughter of a Vampire", // Family.
                    "1,601", // Age.
                    "When Draculaura and I were growing up in Transylvania, I always screamed of becoming an actress. But it turns out I’m also the rightful heir to the Vampire Queen throne. When I’m not home ruling the vampire world, you can find me on the set as my alter ego, movie star Veronica Von Vamp", // Description.
                    "Blood oranges", // Favorite food.
                    "Acting! It might seem strange that I would like acting if I have stage fright, but I love inhabiting the characters I play", // Favorite activity.
                    "I am fangsolutely in love with lace, ruffles, silk and satin, as long as it’s frilly and it’s black or deep purple", // Killer style.
                    "I played the part of a dragon whisperer in a film once. It was a great role, but I’ve never translated that into a real unlife pet", // Pet.
                    "When I flub a line. I work very hard to learn my part so that I do not make mistakes, and it fangsolutely drives me batty when I do", // Pet peeve.
                    "I have terrible stage fright, so I must be completely prepared before I ever stand in front of an audience. I’ve been told I’m a natural on stage and in front of a camera, but that’s only because I spend hours making sure I have my lines down. It’s the only way I can do what I do", // Monster quirk.
                    "Draculaura and Viperine Gorgon" // Friends.
                ),
                new Personajes
                (
                    "Elle", // Resource.
                    "Elle Eedee", // Name.
                    "Daughter of the Robots", // Family.
                    "16", // Age.
                    "I’m constructed from a long lineage of Robots, which means my latest system upgrade always gives me a total edge on the electronic music circuit. Monster High is the perfect place to beta test my clawesome compositions and state-of-the-art dance moves", // Description.
                    "Anything with olive oil. Ctrl – Alt – Delish!", // Favorite food.
                    "Composing. I love turning electronic squelches into clawesome music", // Favorite activity.
                    "The future is now! I love clean lines and shiny metallic fashions", // Killer style.
                    "I am working on a pet for the future, but she is still in beta", // Pet.
                    "When monsters ask me if my intelligence is artificial! Like I have never heard that one before", // Pet peeve.
                    "I can drop a monstrous beat, but when I dance I get too amped up and can sometimes blow a fuse! As much as I love the dance floor, I usually stay behind the turntables where I will not need to ask Frankie for a plug-in to reboot", // Monster quirk.
                    "Luna Mothews, Mouscedes King and Astranova" // Friends.
                ),
                new Personajes
                (
                    "Fawntine", // Resource.
                    "Fawntine Fallowheart", // Name.
                    "", // Family.
                    "", // Age.
                    "Dream Monster: A deer. My friends say I’m a little bit quiet and a little bit shy, and I suppose they’re a little bit right. Although I prefer to think of myself as gentle, which suits me right down to the ground", // Description.
                    "", // Favorite food.
                    "I’m a nature ghoul. My favorite thing to do is simply enjoy the company of all flora and fauna", // Favorite activity.
                    "I like to blend in with nature, so everything I wear is organic, and embellished with flowers, vines and bones", // Killer style.
                    "My beasties understand, but monsters who don’t know me well sometimes mistake my quietness for snobbiness. Nothing could be further from the truth", // Pet.
                    "My beasties understand, but monsters who don’t know me well sometimes mistake my quietness for snobbiness. Nothing could be further from the truth", // Pet peeve.
                    "I love being with other monsters, but only in small groups. Large crowds make me feel like a deer caught in headfrights", // Monster quirk.
                    "Penepole Steamtail and Skyra Bouncegait" // Friends.
                ),
                new Personajes
                (
                    "Finnegan", // Resource.
                    "Finnegan Wake", // Name.
                    "Son of a Merman", // Family.
                    "17", // Age.
                    "Some may think I charge into situations too quickly, but I don’t see anything wrong in living unlife by the skin of my fin! I mean… you only unlive once, right?", // Description.
                    "Kelp smoothies with protein boosts. They taste spoketastic and give me lots of energy so I’m always ready to roll (pun intended)!", // Favorite food.
                    "When I’m not wheeling around school in my chair, you can find me working out in the school pool, at Gloom Beach or in the ocean. Water workouts really keep my fins fit", // Favorite activity.
                    "My blue mohawk and clawesome tats may say punk rocker, but my fierce fashions are scary-cool race wear", // Killer style.
                    "A ghost cheetah would be my skultimate pet, because they’re the fastest creature in the monster world…but they’re too wild to be tamed", // Pet.
                    "Monsters who think that because I’m in a chair, I’ll let anything hold me back. I may be a fish out of water, but I know what I can—and can’t—do", // Pet peeve.
                    "I’m a scaredevil who doesn’t like to be told, “That’s too dangerous.” Though, sometimes I may dive in headfirst too quickly!", // Monster quirk.
                    "Lorna Mcnessie and Gigi Grant" // Friends.
                ),
                new Personajes
                (
                    "Flara", // Resource.
                    "Flara Blaze", // Name.
                    "", // Family.
                    "", // Age.
                    "Dream Monster: A phoenix. Even though I’ve often been accused of fanning the flames of chaos, I like to think what I really do is provide the sparks of transformation that turn old sticks-in-the-mud into Roman candles of celebration", // Description.
                    "", // Favorite food.
                    "I like to throw parties, events for howlidays, and the occasional flash mob! I handle all barbecues, stunts, fireworks and fun… with flair!", // Favorite activity.
                    "Ghoul-la-la Glam! I love to burn bright in dramatic outfits, shimmering in gold – especially when embellished with feathers. Lots and lots of feathers", // Killer style.
                    "", // Pet.
                    "Some ghouls are too intimidated by my internal-fire to fang out with me, but I wish they’d give it a chance. All they have to do is be way more exciting", // Pet peeve.
                    "I have a bad habit of singeing my mane and tail when I get too fired up. Good thing it grows back fast", // Monster quirk.
                    "Skyra Bouncegait, Penepole Steamtail and Fawntine Fallowheart" // Friends.
                ),
                new Personajes
                (
                    "Frankie", // Resource.
                    "Frankie Stein", // Name.
                    "Daughter of Frankenstein", // Family.
                    "How many days has it been now? ", // Age.
                    "I was brought to unlife as a teenager, so I’m still a scary-bit naïve about the ways of the monster world. That’s why I’m always reading Teen Scream magazine...I’m trying to get caught up on teen unlife. I put every spark of energy into school and my extrascarricular activities, like being a member of the Fear Squad, so I can enjoy every second of being a monster teen", // Description.
                    "Everything I've tried is the best thing ever!", // Favorite food.
                    "I don't really have a favorite yet. I want to experience everything before I have to choose", // Favorite activity.
                    "My friends say I have the perfect figure for fashion. They've taken me shopping for some scary-cute clothes that are absolutely to die for", // Killer style.
                    "Watzit™. I'm not sure of all the things he's made from, but Watzit's pet license is 10 pages long", // Pet.
                    "Every morning when I come upstairs, my father insists on grinning and shouting, \"It's alive\"", // Pet peeve.
                    "Sometimes my stitches come loose at the worst possible moments. Like the day my arm flew off and landed right in front of the most creeperific guy. I was mortalfied", // Monster quirk.
                    "Draculaura, Clawdeen Wolf and Jackson Jekyll" // Friends.
                ),
                new Personajes
                (
                    "Frets", // Resource.
                    "Frets Quartzmane", // Name.
                    "", // Family.
                    "", // Age.
                    "Dream Monster: A gargoyle. It’s all about the hoofbeats for me. I’m always trotting out the latest tunes and I love it when one of my friends yells, “Hay, I know that song!”", // Description.
                    "", // Favorite food.
                    "Jamming with my musical beasties. When we get rocking, you can feel the vibrations all the way up in the catacombs", // Favorite activity.
                    "I like to rock leather, studs, and chains, and then soften up the look with a few black roses", // Killer style.
                    "", // Pet.
                    "Since I’m a gargoyle, monsters sometimes think I should be watching over them; but I’m a performer, so I want all eyes on me… and my guitar!", // Pet peeve.
                    "Being made of stone makes me heavy, so my gallop is more like a trot. I’m usually a few steps behind the other ghouls", // Monster quirk.
                    "Pyxis Prepstockings, Bay Tidechaser and Aery Evenfall" // Friends.
                ),
                new Personajes
                (
                    "Garrott", // Resource.
                    "Garrott Duroque", // Name.
                    "Son of a Gargoyle", // Family.
                    "516", // Age.
                    "", // Description.
                    "Strawscary tarts", // Favorite food.
                    "I love to sketch and design fashions. I am like an architect of style", // Favorite activity.
                    "I like to wear the style basics: skinny jeans, scarf, dragon leather jacket and boots. I do this because I wish for all attention to be focused on my designs rather than on moi", // Killer style.
                    "My pets are my roses. I feed them, talk to them, and make sure they receive plenty of sun and water. In return they give to me beauty and inspiration", // Pet.
                    "When some monster takes one of my designs and uses it as their own. To take another monster’s hard work and pretend it is yours is dishonorable", // Pet peeve.
                    "I am a perfectionist. Oui, oui, I know monsters that say this, but I unlive it. If I cannot get every detail on a design the way I think it should look, I will start from the beginning again. It is what makes me a good designer I think", // Monster quirk.
                    "Rochelle Goyle and Clawdeen Wolf" // Friends.
                ),
                new Personajes
                (
                    "Ghoulia", // Resource.
                    "Ghoulia Yelps", // Name.
                    "Daughter of the Zombies", // Family.
                    "16 (in monster years)", // Age.
                    "UHHHHH (Translation: I may not be the fastest ghoul in school, but I am definitely the smartest. I am a freaky-fab problem solver, and I love to help out my friends, especially my beastie Cleo de Nile. I am a comic books fan ghoul; Dead Fast is my favorite. I also like to fang out with my boyfriend Sloman “Slo Mo” Mortavitch)", // Description.
                    "Brains…just kidding. I actually have quite the affinity for rapidly prepared, mass-market cuisine. (Translation: I like fast food)", // Favorite food.
                    "I love to read and learn new things. Books always fit into my schedule", // Favorite activity.
                    "My horn-rimmed \"nerd glasses\". They absolutely go with everything", // Killer style.
                    "My owl, Sir Hoots A Lot™, is the perfect companion—even though he absolutely refuses to be a message courier for me", // Pet.
                    "Last-minute schedule changes and monsters who cannot speak zombie. There is nothing quite so frustrating as arriving late and having to explain why to a monster that does not understand you", // Pet peeve.
                    "I cannot function without a proper schedule and I do not process last-minute changes very well. My zombie nature also means that I walk rather slowly, have trouble making facial expressions and can only speak…well…zombie", // Monster quirk.
                    "Cleo De Nile, Frankie Stein and Clawdeen Wolf" // Friends.
                ),
                new Personajes
                (
                    "Gigi", // Resource.
                    "Gigi Grant", // Name.
                    "Daughter of The Genie", // Family.
                    "Dad says I’m 15, but he lost my birth certificate somewhere between Darius the Great and Julius Caesar, so I’m really not sure", // Age.
                    "I’ve lived most of my unlife in a lantern, only coming out to grant wishes. But now that my shadow sister Djinni “Whisp” has taken over my wish-granting duties, I’m free to do as I wish, which means I can attend Monster High. It’s weird not having my powers, but now I know work hard is another way to make your wishes and screams come true", // Description.
                    "My father’s secret recipe: hummus and fresh-baked pita. Even after all this time, I have never grown tired of it", // Favorite food.
                    "I love sightseeing. It’s always a bonus when a monster wishes to take a trip to someplace I haven’t already been", // Favorite activity.
                    "Natural fabrics, especially silk, in bright colors. I’m also big on haltertops, baggy pants and slippers. Basically, I’m all about comfort. Of course, I can’t be a total slob cause I never know when I’m going to have to pop out for a meeting with the new boss", // Killer style.
                    "A scorpion called Sultan Sting™. Don’t let the name fool you, he’s actually quite unpretentious", // Pet.
                    "That wishing for extra wishes thing. As if 13 wishes isn’t enough", // Pet peeve.
                    "It’s not a real stretch to guess that I’m claustrophobic. Being stuck in a lantern for millennia will do that to you", // Monster quirk.
                    "Howleen Wolf, Twyla, Draculaura, Clawdeen Wolf and Frankie Stein" // Friends.
                ),
                new Personajes
                (
                    "Gilda", // Resource.
                    "Gilda Goldstag", // Name.
                    "Daughter of The Golden Hind", // Family.
                    "16 in Golden Hind years", // Age.
                    "I’m living proof that Golden Hinds aren’t extinct. After all, here I am. While I can run like the wind to escape danger, I also enjoy slowing down for some dead quiet time", // Description.
                    "Wild berry and clover smoothies. I could unlive on them", // Favorite food.
                    "I love to sit next to a quiet shady pool and read a good book. It’s so calm, peaceful and totally free of running", // Favorite activity.
                    "I love the neutral colors of nature! When you’re running through the deep forest you learn to love styles that are sleek and eye- catching as opposed to frilly and branch-catching", // Killer style.
                    "Growing up in the wild, my first friends were animals so I never thought about wanting to have a creature as a “pet”", // Pet.
                    "I hate it when monsters challenge me to race. It’s embarrassing…for them and I don’t like showing up my friends", // Pet peeve.
                    "I guess I’m a bit on the nervous side, especially when it comes to sudden loud noises. They make me want to run first and find out what the noise was second", // Monster quirk.
                    "Venus McFlytrap and Jane Boolittle" // Friends.
                ),
                new Personajes
                (
                    "Gil", // Resource.
                    "Gillington “Gil” Webber", // Name.
                    "Son of the River Monster", // Family.
                    "16", // Age.
                    "My fresh-water monster parents don’t approve of my salt-water girlfriend, and the ocean gives me the chills, but I’m not going to let a few haunting obstacles keep Lagoona Blue and I apart. That may seem like a lot of work for a ghoul, but she’s worth it", // Description.
                    "Seaweed. I know it’s a little exotic for a freshwater monster, but I really love it", // Favorite food.
                    "I love to swim. I know that might seem like a total no-brainer but I really do. I guess it would be the same as a dry land monster who likes to run. It really gives me a chance to think through things and clear my gills", // Favorite activity.
                    "Going from the water to dry land is a big adjustment, so wearing shorts, tank tops and flip-flops helps me feel comfortable when I’m walking around and not swimming. Plus, with my helmet, trying to wear anything with a tight collar is totally out of the question", // Killer style.
                    "I don’t have a pet right now, but I get to hang out with Lagoona’s pet piranha Neptuna™, which is almost like having a pet of my own", // Pet.
                    "When fresh-water monsters talk about salt-water creatures like they are some kind of second-class monsters. We’re all monsters and the way we treat each other means more than what kind of water we swim in", // Pet peeve.
                    "I’m not proud of it but I’m afraid of the ocean--okay not the water exactly, but the monsters that make their home there. I know I shouldn’t be, but it’s taken me a long time to get over something I’ve been afraid of since I was just a small fry. Fortunately for me, Lagoona is helping me get over that fear", // Monster quirk.
                    "Lagoona Blue and Clawd Wolf" // Friends.
                ),
                new Personajes
                (
                    "Gooliope", // Resource.
                    "Gooliope Jellington", // Name.
                    "Daughter of Unknown", // Family.
                    "16", // Age.
                    "Living the nomadic Freak du Chic lifestyle has been a total scream so far, but I’m finally ready to solidify a new home among the student bodies of Monster High. Perhaps I’ll even have the chance to unearth my true monster scaritage?", // Description.
                    "Boba and Jelly Donuts", // Favorite food.
                    "I love putting on ginormous Freak du Chic productions! Building sets, creating costumes… you name it, I do it all! I’m definitely a DIY kind of goo!", // Favorite activity.
                    "Being somewhat larger than unlife makes it impossible to buy clothes “off the rack,” so everything I wear has to be stitched together by claw from material found around the Freak du Chic. That’s not to say my circus chic is not freak du fangtastic, because it certainly is", // Killer style.
                    "There are so many UHHH-mazing creatures I get to help care for in the Freak du Chic that I couldn’t possibly choose a pet without making some creature jealous", // Pet.
                    "When other monsters treat the members of the Freak du Chic like… freaks. Just because we’re not “normal” in the eyes of outsiders doesn’t mean we don’t have the same feelings as everyone else", // Pet peeve.
                    "I’m quite comfortable around the Scarnival and all the terrifyingly talented acts. But when I step outside of this freaktacular world, I feel like I don’t fit in and I become very nervous! So much so, I literally begin to shake like Jelly!", // Monster quirk.
                    "Frankie Stein, Honey Swamp, Jinafire Long and Toralei" // Friends.
                ),
                new Personajes
                (
                    "Headmistress", // Resource.
                    "Headless Headmistress Bloodgood", // Name.
                    "The Headless Horseman", // Family.
                    "Wise enough to be your grandmother—young enough to be your sister", // Age.
                    "", // Description.
                    "Fresh scones with marmalade and a cup of tea. Delightful", // Favorite food.
                    "I love bringing together monsters from different cultures and backgrounds and watching them discover that their commonalities far outweigh their differences", // Favorite activity.
                    "Sable cape, high-collared blouse, riding boots, yard stick and a book on grammar", // Killer style.
                    "Nightmare™ is my horse and constant companion. Although her nervous demeanor belies her rather intimidating name, she is almost always ready to charge into any situation at a moments notice…almost", // Pet.
                    "Young monsters that presume they have nothing left to learn. It is distasteful to see such narrow-mindedness in the young", // Pet peeve.
                    "A lady does not disclose such things in public. It is unseemly. Although I must confess that sometimes my head and body find themselves in separate locations", // Monster quirk.
                    "Abbey Bominable, Clawdeen Wolf, Draculaura, Frankie Stein abd Ghoulia Yelps" // Friends.
                ),
                new Personajes
                (
                    "Heath", // Resource.
                    "Heath Burns", // Name.
                    "Son of The Fire Elementals", // Family.
                    "15", // Age.
                    "The Heathster might burst into flames at the worst possible moments, but that’s because I’m hot! Just ask the ghouls. They love me, especially Abbey Bominable. We have a fire and ice thing going on. Whatever trouble I might create, it’s not on purpose. It usually starts when I’m just trying to help", // Description.
                    "Ghost Chilis, bro. They’re like candy to the Heathster", // Favorite food.
                    "I love playing video games, ‘cause I always get to be the hero and if I make a mistake I can just hit the reset button", // Favorite activity.
                    "I know all the other dudes at MH take their style cues from the Heathster, so I try not to show them up too much. Mostly I keep it casual around the bros. Of course, I have to have my flame-sleeved jacket. The ghouls think it’s hot!", // Killer style.
                    "I’m saving up for a pet dragon. I almost had enough to get one until I had to use the money I’d saved to replace the ice scream machine in the creepateria…don’t ask", // Pet.
                    "When I say, “I got this!” and then I hear another monster say, “Heath! No!” It’s like they totally have no faith in the Heathster", // Pet peeve.
                    "I have a bit of an impulse/self-control issue, which causes me to accidentally burst into flames at kind of the worst possible moments. My dad also says my attention span is so short that….Hey! What was that?", // Monster quirk.
                    "Deuce Gorgon and Clawd Wolf" // Friends.
                ),
                new Personajes
                (
                    "Holt", // Resource.
                    "Holt Hyde", // Name.
                    "Son of Mr. and Mrs. Hyde", // Family.
                    "16…I think", // Age.
                    "Music brings me to unlife—literally. When Jackson Jekyll hears a few notes from any fierce song, he is transformed into me, the hottest DJ at Monster High. The only thing the two of us have in common, though, is we both have a crush on Frankie Stein", // Description.
                    "Hot wings. I like ‘em hot enough to make a dragon cry", // Favorite food.
                    "Creating new monster music mixes. I love it when my tunes come alive and I can get all the monsters in the house out on the dance floor", // Favorite activity.
                    "My style is on fire, baby. No really, I literally have flames leaping off my body. I’m like my own light show. I also have this smokin’ yin-yang symbol tattooed on my back and, of course, I never go anywhere without my headphones ‘cause you never know when a beat will blaze up", // Killer style.
                    "Crossfade™, a chameleon. He’s the best pet ever ‘cause change never bothers him, and he totally digs my music", // Pet.
                    "Having to listen to boring music. Seriously, if music can’t transform you, it ain’t nothing but static", // Pet peeve.
                    "I’ve got a bad temper. It’s not something I’m proud of but every once in a while it flares up and I have to spend a lot of time apologizing for it. I also have a hard time remembering some things", // Monster quirk.
                    "Operetta, Catty Noir, Clawd Wolf and Jackson Jekyll" // Friends.
                ),
                new Personajes
                (
                    "Honey", // Resource.
                    "Honey Swamp", // Name.
                    "Daughter of The Honey Island Swamp Monster", // Family.
                    "115 in swamp monster years", // Age.
                    "I’m a proper Southern ghoul from the swamps of Honey Island. My mama always taught me if you work hard and follow your screams, you can be anything you want to be. Well, I want to be a world-famous cinema-togre-pher in Hauntlywood", // Description.
                    "Dead beans and rice and jamboolya", // Favorite food.
                    "I am a photographer. I do not want to be one, you understand; I am one. I am always striving to learn more and be…perfect", // Favorite activity.
                    "I am sweet in disposition, polite in manners, feminine in appearance and sociable in company. In short, I epitomize the modern Southern ghoul. I am also strong, independent and capable--you would do well to remember that", // Killer style.
                    "Living in the swamp means my home is literally crawling with critters. Unfortunately I haven’t been able to find pet perfection. I suppose that means I’ll need to keep looking", // Pet.
                    "I do not like to be rushed. Please allow for adequate time when asking me to go somewhere with you", // Pet peeve.
                    "I am a perfectionist. I believe there is always time to do something the right way and yes I realize this can be tiresome for other monsters", // Monster quirk.
                    "Clawdia Wolf and Viperine Gorgon" // Friends.
                ),
                new Personajes
                (
                    "Howleen", // Resource.
                    "Howleen Wolf", // Name.
                    "Daughter of The Werewolf", // Family.
                    "14", // Age.
                    "I’m dying for my older siblings to stop treating me like I’m a baby. Clawdeen is always telling me how to dress, and I just wish she’d let me figure out my own killer style (including my constantly changing hair). Thankfully, I have my beastie Twyla, and we are navigating the horrors of high school together", // Description.
                    "Hot dogs", // Favorite food.
                    "I love playing soccer. It’s so much fun", // Favorite activity.
                    "I like to call the way I dress “were-punk.” It’s like taking lots of different styles and mashing them together to make something totally new and creepy cool. Clawdeen says it always looks like I got dressed in the dark, but I like it and that’s all that matters to me", // Killer style.
                    "Cushion™ is my pet hedgehog. She may be a little prickly on the outside, but she’s really sweet on the inside and that’s what matters", // Pet.
                    "When I get treated like the little sister. I mean I am the little sister, but I’m not a baby anymore", // Pet peeve.
                    "My hair. Sometimes it does what I want, sometimes it does what it wants and sometimes it does things that make both of us look bad", // Monster quirk.
                    "Abbey Bominable, Clawdeen Wolf, Toralei, Twyla and Gigi Grant" // Friends.
                ),
                new Personajes
                (
                    "InvisiBilly", // Resource.
                    "Invisi Billy", // Name.
                    "Son of The Invisible Man", // Family.
                    "15", // Age.
                    "If I concentrate hard enough, I can disappear on cue…at least some of the time. Even when I am invisible, though, my ghoulfriend Scarah Screams knows I’m there, because she can read my mind", // Description.
                    "I love seafood and pie", // Favorite food.
                    "I love doing special effects for MH theatre productions. It’s scary cool when the audience ooh’s and ahh’s over an effect, and it’s clear they can’t figure out how it was done. Oh, and hanging out with my ghoulfriend Scarah Screams", // Favorite activity.
                    "I totally unlive in my hoodie, skinny jeans and beanie, so I guess if I had to give how I dress a name it would probably be “blister”", // Killer style.
                    "I had a dog once but I had to give him away because every time I took him out for a walk, animal control would try and pick him up as stray", // Pet.
                    "When monsters think I’m a ghost. I’m just invisible, okay. I can’t pass through walls unless there are open doors in them", // Pet peeve.
                    "I can only become visible when I really concentrate and even then it’s not always my whole body. Sometimes it’s just my hand or foot", // Monster quirk.
                    "Scarah Screams, Twyla, Ghoulia Yelps and Deuce Gorgon" // Friends.
                ),
                new Personajes
                (
                    "Iris", // Resource.
                    "Iris Clops", // Name.
                    "Daughter of The Cyclops", // Family.
                    "15", // Age.
                    "", // Description.
                    "Black-eyed peas", // Favorite food.
                    "I love star-gazing. It’s just so clawesome to look up into the night sky and wonder who or what is out there", // Favorite activity.
                    "I think I have a very discerning eye for fashion and it’s easy for me to really focus in on the designs and styles that beast suit me. I especially love fabrics with eye patterns on them", // Killer style.
                    "I really want an alien pet of some kind--preferably something multi-eyed and fuzzy not multi-tentacled and oozy", // Pet.
                    "Not being able to find a pair of stylish, off the rack sunglasses. I mean I can’t be the only fashion-forward Cyclops who thinks UV protection shouldn’t just be utilitarian", // Pet peeve.
                    "It’s easy to see by the bumps and bruises I’m constantly nursing that I’m more than just a little clumsy. I like to blame it on my lack of depth perception, but I think it’s mostly because I don’t always watch where I’m going", // Monster quirk.
                    "Gigi Grant" // Friends.
                ),
                new Personajes
                (
                    "Isi", // Resource.
                    "Isi Dawndancer", // Name.
                    "Daughter of a Deer Spirit", // Family.
                    "16", // Age.
                    "I come from a long line of Boo Hexican Deer Spirits who have learned through the centuries to trust in our mysterious visions. Mine recently brought me to Monster High, where I hope to connect with students through the dance of my ancestors", // Description.
                    "Apples and honey", // Favorite food.
                    "I love to dance above all other things, and any excuse to dance is a good one", // Favorite activity.
                    "I love to mix tribal patterns with fringe and eye-popping colors to create a modern look that stays true to my monster heritage. I also like my fashions to be flexible and comfy because I’m prone to breaking out in spontaneous dance", // Killer style.
                    "The birds of the air and beasts of the field are all my friends. I would not want to cause jealousy by choosing one over the other", // Pet.
                    "I hate having bright lights shined in my face. They make me want to stand still and run at the same time", // Pet peeve.
                    "It is kind of embarrassing, but I have this effect on mansters that makes them fall hoof-over-antler for me. Unfortunately, they usually end up falling on their hearts", // Monster quirk.
                    "Twyla and Gilda Goldstag" // Friends.
                ),
                new Personajes
                (
                    "Jackson", // Resource.
                    "Jackson Jekyll", // Name.
                    "Son of Dr. and Mrs. Jekyll", // Family.
                    "16", // Age.
                    "I may seem like a responsible normie, but all it takes is a few notes to transform me into Holt Hyde…and all bets are off. I hate not being reliable, because I never know when I’m going to become Holt. Worst of all, I never remember what happened when I regain my identity", // Description.
                    "Macaroni and cheese", // Favorite food.
                    "I’m actually pretty athletic, not like werewolf athletic of course, but I like playing basketball—I mean casketball—and video games", // Favorite activity.
                    "I like sweater vests and plain styles that are neat and orderly and buttoned down. It may seem a little quiet and dull, but the last thing I need in my life is more loud and flashy", // Killer style.
                    "Crossfade™, a chameleon. He can blend into any situation and never call attention to himself", // Pet.
                    "Having to listen to loud music with a beat. It causes me to lose my short-term memory and my long-term dignity", // Pet peeve.
                    "My dual nature makes it impossible for me to be totally reliable. It’s not that I don’t try, but it’s like a total creep shoot whether or not I actually follow through on any promise I make", // Monster quirk.
                    "Frankie Stein, Deuce Gorgon, Clawd Wolf and Heath Burns" // Friends.
                ),
                new Personajes
                (
                    "Jane", // Resource.
                    "Jane Boolittle", // Name.
                    "Daughter of Doctor Boolittle", // Family.
                    "15 or 16", // Age.
                    "I spent my formative years in the jungle, so I learned early howl to talk to animals. However, I never really got to interact with monsters my own age. That’s why I came to Monster High…to become more accustomed to unlife with other teenagers. But truth be told, I still enjoy hanging out with animals, too, especially the Secret Creeper pets", // Description.
                    "Mangoes. When I can’t get them fresh, I’ll eat the dried ones--delish", // Favorite food.
                    "I love a good trek. There’s nothing quite as peaceful and invigorating as exploring the wilderness and meeting the creatures that live there", // Favorite activity.
                    "I suppose one would call my style jungle chic. I simply adore faux fur, animal prints and feathers. I also have an absolutely smashing walking stick I cannot stand to be without", // Killer style.
                    "Needles™ is my pet voodoo sloth. I love him dearly, but he is always getting stuck, so I constantly must make a point of telling him to be scareful", // Pet.
                    "When monsters want me to eavesdrop on the conversations between their companion creatures and other animals. Honestly, it’s no less rude than if I were listening in on that monster’s conversation with one of her friends", // Pet peeve.
                    "Spending my formative fears on a jungle island with only the companionship of animals did little to prepare me for the company of other monsters in large groups. When I find myself in that situation, my pulse races, I breathe very fast, and I start looking for places to hide", // Monster quirk.
                    "Headless Headmistress Bloodgood" // Friends.
                ),
                new Personajes
                (
                    "Jinafire", // Resource.
                    "Jinafire Long", // Name.
                    "Daughter of A Chinese Dragon", // Family.
                    "15 hundred scales", // Age.
                    "", // Description.
                    "Anything Sichuan! The spicier it is, the better", // Favorite food.
                    "I love to do calligraphy. It’s very calming and helps me relax", // Favorite activity.
                    "I like to take traditional fashions and fire them up with sharp cuts and fierce accents", // Killer style.
                    "I do not currently have a pet because it is a decision that takes many years for dragons to make. I have narrowed my list down to 50 so I expect that in another couple hundred years, I’ll have a top 20", // Pet.
                    "My tail gets in the way sometimes and it’s really hard to find haunt couture fashions that accommodate it", // Pet peeve.
                    "I’m strong willed, hot tempered and I have a tendency to set fire to combustible materials although ever since I melted my last iCoffin, I’ve been trying to do a better job of controlling my impulses", // Monster quirk.
                    "Skelita Calaveras, Clawdeen Wolf, Draculaura and Frankie Stein" // Friends.
                ),
                new Personajes
                (
                    "Kala", // Resource.
                    "Kala Mer’ri", // Name.
                    "XXXXX Shhh! It’s a secret! ", // Family.
                    "16", // Age.
                    "I may keep my sea monster heritage a secret, but that doesn't mean I'm likely to fade into the shadows. Actually, I like to think of myself as pretty bold - from my bright threads to my freaky-fab dance moves, I love to be the one who's always swimming circles around the competition", // Description.
                    "Every Down Under ghoul loves some Shrimp on the Barbie", // Favorite food.
                    "I am a natural performer and love to dance all my scares away!", // Favorite activity.
                    "I like bold, bright colors that allow me to stand out in a sea of monsters. I don’t want to be yet another drop in the ocean; I want to be the visual eye of the storm", // Killer style.
                    "Do Peri and Pearl count? Just kidding! I don’t have time for pets", // Pet.
                    "When other monsters think they know you based on shallow stereotypes and underwater legends", // Pet peeve.
                    "I need to have my tentacles on everything. Some of my beasties say it’s because I’m a control shriek, but I need to feel secure and in charge of my unlife", // Monster quirk.
                    "Peri & Pearl Serpentine" // Friends.
                ),
                new Personajes
                (
                    "Kiyomi", // Resource.
                    "Kiyomi Haunterly", // Name.
                    "Daughter of The Noppera-Bo (Faceless Ghost)", // Family.
                    "16", // Age.
                    "", // Description.
                    "Grandmother’s homemade udon. It is a meal to die for!", // Favorite food.
                    "I love to observe other creatures going about their daily unlives. I also find it enjoyable to see their reactions when I match my face to theirs", // Favorite activity.
                    "I like fashions that are comfortable and ''floaty.'' They are very practical if you are going to be doing a lot of hovering", // Killer style.
                    "A baby kaiju. My parents suggested a kitsune since kaiju are loud and somewhat destructive. I think he is kawaii though, even if he thinks roaring is more frightening than silently appearing", // Pet.
                    "I dislike it when creatures complain out loud. It is tiresome and not honorable", // Pet peeve.
                    "I have always been a reserved and quiet spirit, preferring to allow my appearance to say what needs saying. However, I do change shades of color based on what emotion I am currently feeling…", // Monster quirk.
                    "Spectra Vondergeist and Draculaura" // Friends.
                ),
                new Personajes
                (
                    "Kjersti", // Resource.
                    "Kjersti Trollson", // Name.
                    "Daughter of a Troll", // Family.
                    "14 in Troll years", // Age.
                    "Go ahead and call me a gamer-ghoul… I’m totally cool with that! Let’s just say I know how to keep my eye on the prize and plan on pulling off a few fangtastic level ups now that I’m enrolled at Monster High", // Description.
                    "Screechza. It is the perfect gamer food", // Favorite food.
                    "I love playing video games with monsters from all over the world. Even though we’ve never met face-to-face, I feel as if they are my friends", // Favorite activity.
                    "I am a gamer and I like giving my wardrobe a turbo boost of cute with scary sweet game-themed skirts, t-shirts and shoes. I enjoy the feeling of being fashionable, especially when I’m kicking another gamer’s character all over the screen", // Killer style.
                    "Would it be odd if I said my video game console?", // Pet.
                    "Server lag. One minute your character is sneaking up behind another monster’s character, then the screen freezes and you are the one re-spawning", // Pet peeve.
                    "I am a control freak and more than a bit of a nerd. I think that’s why I like gaming, because I can make the characters I’m playing do whatever I “tell” them to do, and if they don’t I can always reset the game. Alas, there are no reset buttons in real unlife", // Monster quirk.
                    "Ghoulia Yelps and Heath Burns" // Friends.
                ),
                new Personajes
                (
                    "Lagoona", // Resource.
                    "Lagoona Blue", // Name.
                    "Daughter of the Sea Monster", // Family.
                    "15", // Age.
                    "I’m a sea monster from the Great Scarrier Reef and captain of the swim team. My boyfriend Gillington “Gil” Webber is a fresh-water sea monster, but that’s a whole other complicated story. I’m pretty laid-back, except when it comes to scary important causes I believe in, like protecting the environment", // Description.
                    "Sushi", // Favorite food.
                    "Anything that involves being in, on or around the water", // Favorite activity.
                    "I mostly like to creep out in my baggies, tank top and floppies. That way, I'm ready for any kind of scary-good time—whether it's surfing, beach volleyball or hanging out with my ghouls. But I also like to show up to parties in my scary-cute little black dress just to show everybody I can pull off the lurk", // Killer style.
                    "Neptuna™ is my pet piranha, and I've got a special purse that's actually a fishbowl so I can secretly take Neptuna to class", // Pet.
                    "Anyone who treats the ocean like it's his or her own personal trash can", // Pet peeve.
                    "My skin tends to dry out if I spend too much time out of the water, so I go through a fright of moisturizer. Chlorine from the Monster High pool also has a tendency to turn my blond hair blue, but it's a creeperfic lurk, don't you think?", // Monster quirk.
                    "Frankie Stein, Clawdeen Wolf, Draculaura, Ghoulia Yelps, Cleo De Nile, Deuce Gorgon and Abbey Bominable" // Friends.
                ),
                new Personajes
                (
                    "Lorna", // Resource.
                    "Lorna McNessie", // Name.
                    "Daughter of the Loch Ness Monster", // Family.
                    "14", // Age.
                    "", // Description.
                    "Haggis. If there were ever a dish made for a monster, it’d be this", // Favorite food.
                    "Aye, it’s photo bombing, but nae just the ordinary kind. I like tae find ways tae get a shot that are nae so obvious. So ya might nae see me when you look at a picture the fiercst time", // Favorite activity.
                    "Being a ghoul from the Highlands, I’d be doing a disservice tae ma clan if I didna wear ma plaid, and since it can also get shivering Baltic in Rotland I also like tae wear wool skirts and scarfs tae keep the cold at bay", // Killer style.
                    "I always wanted a dry land pet like a Highland cow, but when you live in a loch there’s nae place tae keep one", // Pet.
                    "Normie expeditions tae find aboot if we really exist, because every time they bring in a submarine or underwater camera or sonar we have tae leave the loch till they’re gone. It’s no fun tae move and I don’t get a chance tae be in a shot", // Pet peeve.
                    "I never met a photo I didna want tae be a part of, much tae the consternation of ma parents who are notoriously photo shy. It’s nae like I want tae ruin a shot, but it’s just so much fun", // Monster quirk.
                    "Gillington “Gil” Webber and Marisol Coxi" // Friends.
                ),
                new Personajes
                (
                    "Luna", // Resource.
                    "Luna Mothews", // Name.
                    "Daughter of the Moth Man", // Family.
                    "16", // Age.
                    "", // Description.
                    "Red velvet cupcakes with scream cheese frosting", // Favorite food.
                    "Singing, dancing, acting, so many things… I couldn’t possibly choose just one!", // Favorite activity.
                    "Total goth-moth! I love darks with bright pops of color, just like the city at night!", // Killer style.
                    "I don’t think there’s a pet undead that could keep up with my hectic schedule", // Pet.
                    "When ghoulfriends won’t lend me their clothes because they think I’ll return them all moth-eaten", // Pet peeve.
                    "I am attracted to stardom. For me, that is the light that shines the brightest and I am determined to reach it. But I’m also dazzled by my beasties, and I don’t mind getting a little off course if it means lending a helping wing to one of my ghoulfriends", // Monster quirk.
                    "Mouscedes King, Elle Eedee and Astranova" // Friends.
                ),
                new Personajes
                (
                    "Madison", // Resource.
                    "Madison Fear", // Name.
                    "Daughter of Sirens", // Family.
                    "14", // Age.
                    "", // Description.
                    "Cookies. They are the perfect food", // Favorite food.
                    "Singing is my unlife, and I can’t help but burst into song on just about any occasion where it’s called for -- sometimes…hehe…even when it’s not", // Favorite activity.
                    "I am a total ghoul at heart and I love scary-sweet styles that are a reflection of that, but I also like to be a little edgy, too, ‘cause even the calmest seas still make waves", // Killer style.
                    "I have a golden doodle who is one part fancy, one part adventurous, and all parts sweet", // Pet.
                    "I don’t like bullying, and my heart just breaks when I see the damage being bullied causes. I also hate when monsters think I use my voice and my songs to lure monsters to their doom because I am a Siren monster. Nothing could be further from the truth. I use my voice like a lighthouse to guide monsters from the storms of unlife to safe harbors of peace and calm", // Pet peeve.
                    "I’m kind of clumsy when I’m on land. It’s like in my mind I mean to do one thing, but somehow my body doesn’t get the message. It’s totes embarrassing, too, especially when I have an epically clumsy fall in front of other monsters", // Monster quirk.
                    "Frankie Stein and Draculaura" // Friends.
                ),
                new Personajes
                (
                    "Marisol", // Resource.
                    "Marisol Coxi", // Name.
                    "Daughter of the South American Bigfoot", // Family.
                    "17", // Age.
                    "", // Description.
                    "Ceviche is my favorite food by far and especially I love the leche de tigre that's left over at the bottom of the bowl", // Favorite food.
                    "I love getting new nail art! It is the most perfect way to accent my best features and what ghoul doesn't like getting a mani-pedi?", // Favorite activity.
                    "I like BIG and LOUD! Colors, fashions, music, hair - everything goes to ''12'' with me except my feet. They're way off the chart because they're simply too gore-geous to be measured. So when you see me coming (and I can't be missed in my colorfully modern take on the traditional Monster Picchu fashions), just remember ''Big Hair Don't Scare''", // Killer style.
                    "I love llamas even though I do not have one as a pet. I mean, how could I not love them? They are fuzzy and quirky and are as much a part of Monster Picchu as I am", // Pet.
                    "Shoe stores that do not carry my size. My feet are a work of art, so is it too much to ask for a store to carry a variety of stylish ''frames'' in my size?", // Pet peeve.
                    "My parents say that even when I whisper, I'm still about as quiet as an avalanche, but I am simply larger than unlife and I cannot hide - not that I would want to - what I am feeling inside", // Monster quirk.
                    "Lorna Mcnessie and Abbey Bominable" // Friends.
                ),
                new Personajes
                (
                    "Mouscedes", // Resource.
                    "Mouscedes King", // Name.
                    "Daughter of the Rat King", // Family.
                    "15", // Age.
                    "I may come from Boo York royalty, but I’m no Upper Beast Side snob. Yes, I’m a rabid shopper with a talent for finding all the best sales, but what really gets my tail tapping is a beat I can dance to and furrrocious friends by my side", // Description.
                    "A nice sharp cheddar rice cheese always hits the spot!", // Favorite food.
                    "Any kind of dancing. Even my tail has some monster moves", // Favorite activity.
                    "Total Upper Beast Side!", // Killer style.
                    "I’d like to have a pet someday. Maybe something tiny and cute that I can carry in my purse", // Pet.
                    "The stigma against us rats, which is based on scary-tails. I used to be embarrassed to be the Rat King’s daughter, but now I know that being a monster princess totes rules!", // Pet peeve.
                    "I’m a rat ghoul who is lactose intolerant – which sounds like some kind of cheesy joke. I try not to be a pest, but I can’t help it if I need to order my extra-cream, extra-whip no-dairy latte: substitute soy, hold the cream and easy on the foam", // Monster quirk.
                    "Luna Mothews, Elle Eedee and Astranova" // Friends.
                ),
                new Personajes
                (
                    "Nefera", // Resource.
                    "Nefera De Nile", // Name.
                    "Daughter of The Mummy", // Family.
                    "I am ageless, of course…but for those of you who keep track of such tings, I am three years older than my younger sister Cleo", // Age.
                    "", // Description.
                    "Almas caviar and white truffles", // Favorite food.
                    "Making sure that my sister knows her place in the royal line of succession. It’s right behind me", // Favorite activity.
                    "I prefer fashions and accessories that accentuate my timeless beauty, like the blue of the eternal Nile or gold—especially gold—which does not tarnish or rust. Much like myself", // Killer style.
                    "Azura™ is my pet scarab, Scarabaeus sacer, to be exact. He brings me the sun", // Pet.
                    "If someone or something annoys me, I deal with it. Immediately", // Pet peeve.
                    "Flawed? Who said I was flawed? I want names and a mirror—quickly!", // Monster quirk.
                    "Cleo De Nile, Clawdeen Wolf, Draculaura, Frankie Stein and Ghoulia Yelps" // Friends.
                ),
                new Personajes
                (
                    "Neighthan", // Resource.
                    "Neighthan Rot", // Name.
                    "Son of a zombie (father) and a unicorn (mother)", // Family.
                    "17", // Age.
                    "", // Description.
                    "Fried pickles and brain puffs--not at the same time because that would be nasty", // Favorite food.
                    "I like hanging out with my beast friends. I know that doesn’t sound adventurous or exciting, although it can be, but I‘m a social monster and it’s what I like to do", // Favorite activity.
                    "I like spike details to match my horn and bold bright colors because they do a better job of camouflaging the results of my spills and clumsiness. Also, and I hope this doesn't come across as arrogaunt but I think my mane and tail look pretty clawesome", // Killer style.
                    "As clumsy as I am, I think the only pet I’d trust myself with is a mastodon, and I hear they take up a lot of room", // Pet.
                    "When monsters make judgments about other monsters based only on what a monster looks like. It’s not fright and I don’t like it", // Pet peeve.
                    "There is no hiding the fact that I am just flat-out clumsy and some days I think I could actually trip over the wind. If you need something dropped, spilled or broken. I’m definitely your manster", // Monster quirk.
                    "Sirena Von Boo, Avea Trotter and Bonita Femur" // Friends.
                ),
                new Personajes
                (
                    "Operetta", // Resource.
                    "Operetta", // Name.
                    "Daughter of The Phantom of the Opera", // Family.
                    "16…in phantom years", // Age.
                    "", // Description.
                    "Fried peanut butter and banana sandwiches, thankyouverymuch", // Favorite food.
                    "Playing music and singing. What else is there to unlife?", // Favorite activity.
                    "From the top of my hotrod red victory roll hairdo to the soles in my shoes—don’t step on ‘em—I’m a high-octane rockabilly phantom de force. I’ve also got this pitch-perfect beauty mark that starts on my face and ends in the ginchiest tat ever", // Killer style.
                    "Memphis “Daddy O” Longlegs™ is my pet spider. Course he’s not like any other spider you’ve ever seen, unless you’ve seen one rocking a pompadour while playing a standup bass", // Pet.
                    "I don’t like being told what to do—guess I’m hardheaded that way", // Pet peeve.
                    "I’m a bit of a diva and a perfectionist…okay more than a bit. Mostly, it’s just about my music though, which causes monsters to kinda lose their minds for a few days if they hear me sing live. My voice doesn’t have the same effect when you listen to it recorded", // Monster quirk.
                    "Deuce Gorgon, Holt Hyde and Catty Noir" // Friends.
                ),
                new Personajes
                (
                    "Penepole", // Resource.
                    "Penepole Steamtail", // Name.
                    "", // Family.
                    "", // Age.
                    "Dream Monster: A butterfly. I’m a finely tuned Fright-Mare who’s geared to trot like a top. I believe precision makes the world go round and that includes getting my name right. It’s P-E-N-E-P-O-L-E! Please pronounce it precisely", // Description.
                    "", // Favorite food.
                    "I create clockwork pieces that are part functional, part art. All my beasties have received at least one as a gift", // Favorite activity.
                    "Screampunk! I like to take 19th century fashions, add bits of brass – like gears and maybe some goggles – and make something mane-ificent!", // Killer style.
                    "", // Pet.
                    "Don’t mistake my love of all-things-vintage to mean I’m not a forward-galloping ghoul", // Pet peeve.
                    "It’s a challenge for me to prance-straight through other monsters’ dreams where time gets so muddled", // Monster quirk.
                    "Skyra Bouncegait and Fawntine Fallowheart" // Friends.
                ),
                new Personajes
                (
                    "Peri_Pearl", // Resource.
                    "Peri & Pearl Serpentine", // Name.
                    "The Hydra", // Family.
                    "16", // Age.
                    "We're a two-headed Hydra with two times the personality! We don't always see eye-to-eye, in fact most of the time we're butting heads over something, but hey... isn't that what sisters do? Luckily, having a twin sister attached to your hip means one scary-cool thing: your beast friend is always by your side", // Description.
                    "We like finger foods because we can eat at the same time. Anything that requires 2 hands to get to each mouth can take a while", // Favorite food.
                    "Fanging out with each other! Which is a good thing, don’t you think?", // Favorite activity.
                    "Our motto is: “Double the baubles is twice as nice.” We love to accessorize, and we’re always on the hunt for hidden gems to bling-up our 2-of-a-kind outfits", // Killer style.
                    "The heads of our family won’t let us get any pets until we can agree on what kind", // Pet.
                    "When we are treated like we’re the same monster; we have our own interests, screams and personalities. (Pearl wrote that. I don’t really mind it. –Peri)", // Pet peeve.
                    "We constantly butt heads! Just because we’re this close, doesn’t mean we see eye to eye. But we are sisters and beasties, so we always have each other’s back!", // Monster quirk.
                    "Kala Mer’ri" // Friends.
                ),
                new Personajes
                (
                    "Porter", // Resource.
                    "Porter Geiss", // Name.
                    "Son of a Poltergeist", // Family.
                    "16", // Age.
                    "", // Description.
                    "Pickles. Sweet, sour or spicy, I relish them in every form", // Favorite food.
                    "Painting. I'll make anything a canvas! A wall, a floor, my shoes…. If it's blank, I'll paint on it! But only with ghost paint, of course!", // Favorite activity.
                    "I like to wear my art. It creates a fashion statement that is uniquely me. I strive to be an original monsterpiece.", // Killer style.
                    "A raccoon named Huebert. He’s almost as mischievous as I am", // Pet.
                    "When monsters call me a vandal. I'm not a vandal. When I paint on something I don't own, I use ghost paint so it evaporates. But I hope my message will haunt the viewer's memory long after it vanishes", // Pet peeve.
                    "I tend to express myself through my art, which some critics don’t always appreciate. I guess you could say I don’t say it, I spray it", // Monster quirk.
                    "River Styxx, Vandala Doubloons and Kiyomi Haunterly" // Friends.
                ),
                new Personajes
                (
                    "Posea", // Resource.
                    "Posea Reef", // Name.
                    "Daughter of Poseidon", // Family.
                    "17", // Age.
                    "I'm a Great Scarrier Reef sea goddess in training and, if you ask me, a real \"go with the flow\" kinda ghoul. I'm fiercely passionate about tending to the scary-cool seascape my dad has created and am honored that he trusts me with it's care", // Description.
                    "Saltwater taffy! Chewy-gooey yummy!", // Favorite food.
                    "Gardening. I love being out in the seashine, tending my bootiful aquatic plants", // Favorite activity.
                    "I’m a “go with the flow” kind of ghoul. Literally and fashionably. I love to feel flowing fabric and accessories swirl and sway around me, as though they have a life of their own", // Killer style.
                    "All the creatures of the sea are my friends, and I would never consider any of them “pets”", // Pet.
                    "When monsters swim from their fears. Maybe it’s not so much a peeve as something that makes me sad… and determined to help", // Pet peeve.
                    "I know a lot about… a lot! But my knowledge can get tangled like kelp in my head. I know it’s there somewhere, but I can’t always fish it out", // Monster quirk.
                    "Peri & Pearl Serpentine" // Friends.
                ),
                new Personajes
                (
                    "Purrsephone_Meowlody", // Resource.
                    "Purrsephone and Meowlody", // Name.
                    "Daughters of The Werecats", // Family.
                    "15—We’re 15", // Age.
                    "", // Description.
                    "Ice cream", // Favorite food.
                    "When we’re not napping, we love gymnastics—especially because we always land on our feet", // Favorite activity.
                    "Purrsephone: We purrfurr skirts, vests and shiny, jingly bracelets. We also love fashions that match. Right sister?\\par Meowlody: You’re sooo correct. It make life so much less confusing…for us", // Killer style.
                    "We used to have a canary…but one of us is allergic to birds, and we had to give him away", // Pet.
                    "Purrsephone: When monsters automatically assume we think alike and always agree on everything.\\par Meowlody: That’s not my pet peeve. I don’t like closed doors.\\par Purrsephone: Well, you could just open them.\\par Meowlody: That’s not the point", // Pet peeve.
                    "Purrsephone: Sometimes our curiosity gets us into trouble and we agree to purrticipate in the plans of others without considering the consequences.\\par Meowlody: Why sister, you make it sound like we don’t have a mind of our own.\\par Purrsephone: My apologies, purrhaps I was mistaken then", // Monster quirk.
                    "Toralei, Catty Noir and Catrine Demew" // Friends.
                ),
                new Personajes
                (
                    "Pyxis", // Resource.
                    "Pyxis Prepstockings", // Name.
                    "", // Family.
                    "", // Age.
                    "Dream Monster: A pegasus. I’m a classically trained trotter and I love precision mixed with artistry. Watching me go through my paces is like spending a night at the ballet", // Description.
                    "", // Favorite food.
                    "Polo. I’m a sporty ghoul who comes from a long breed of champion polo players. I was swinging a mallet when I was only a foal", // Favorite activity.
                    "My beasties say I’m a preppy dresser, but I’m happy in anything as long as I can accessorize with blue ribbons", // Killer style.
                    "", // Pet.
                    "No, I don’t play water polo. I’m not even sure what that is. Stop asking me – please and thank you.", // Pet peeve.
                    "It’s against the rules of polo to fly, but sometimes I get carried away by the exhilaration and then I have to take a penalty", // Monster quirk.
                    "Frets Quartzmane, Bay Tidechaser and Aery Evenfall" // Friends.
                ),
                new Personajes
                (
                    "River", // Resource.
                    "River Styxx", // Name.
                    "Daughter of The Grim Reaper", // Family.
                    "14", // Age.
                    "", // Description.
                    "CANDY! I have such a sweet bone! Sugar skulls… no… rot chocolate, no…licorice chains ... oh, I can't pick a favorite!", // Favorite food.
                    "Party planning! Food, decorations, entertainment – I love everything that goes into a well-executed…hehe…party", // Favorite activity.
                    "So sweet it'll make your bones hurt! ;P I love candy colors swirled together!", // Killer style.
                    "A raven skeleton named Cawtion – he never fails to come when I “caw” him", // Pet.
                    "Party poopers! I mean, come on! If you can’t rise up for a party, you’re just dead inside", // Pet peeve.
                    "I have a lot of energy, which makes it hard for me to float still, and I pop in and out of sight, which tends to make monsters jump…a lot. Especially when I pop in right behind them! I don’t really think it’s as big a flaw as some other monsters do", // Monster quirk.
                    "Draculaura, Porter Geiss and Vandala Doubloons" // Friends.
                ),
                new Personajes
                (
                    "Robecca", // Resource.
                    "Robecca Steam", // Name.
                    "Daughter of A Mad Scientist", // Family.
                    "116", // Age.
                    "", // Description.
                    "Even though I do not require traditional food, I am fond of ginger tea", // Favorite food.
                    "I am quite the scaredevil, and I delight in using my rocket boots to do stunts and tricks that make other monsters stop what they are doing to watch me perform", // Favorite activity.
                    "I would have described my style as rather old- fashioned in this current time although I have been recently informed that it was totally “steam punk” and quite “cutting edge.” This is a relief as no monster wishes to be thought of as dull", // Killer style.
                    "Captain Penny™ is my mechanical penguin. Working wings are unnecessary when one is equipped with a rocket pack", // Pet.
                    "Rain", // Pet peeve.
                    "My internal clock does not properly function, and I seem to always arrive late. It seems not to matter how many alarms I set or watches I wear; nothing helps. Good thing I have a permanent mechanics excuse in the Headmistress’ office", // Monster quirk.
                    "Frankie Stein, Draculaura, Clawdeen Wolf and Rochelle Goyle" // Friends.
                ),
                new Personajes
                (
                    "Rochelle", // Resource.
                    "Rochelle Goyle", // Name.
                    "Daughter of The Gargoyles", // Family.
                    "415", // Age.
                    "", // Description.
                    "Hard rock candy", // Favorite food.
                    "Sculpting", // Favorite activity.
                    "Being from Scaris, I love to mix the rot iron and stained glass together. It gives my look a certain timeless quality, does it not?", // Killer style.
                    "A gargoyle griffin. She is called Roux™ and she is mine from the time she was hatched", // Pet.
                    "Pigeons", // Pet peeve.
                    "I am very protective of my friends and sometimes I get in the way when they do not have a need for my protection. I also have the…how you say…”chip on the shoulder”", // Monster quirk.
                    "Ghoulia Yelps, Robecca Steam and Clawdeen Wolf" // Friends.
                ),
                new Personajes
                (
                    "Scarah", // Resource.
                    "Scarah Screams", // Name.
                    "Daughter of The bean si (Banshee)", // Family.
                    "15", // Age.
                    "", // Description.
                    "Coddle. ‘Tis the ultimate creepy comfort food", // Favorite food.
                    "I’ve a fair voice and can really wail when I want, oh and I dearly love the listening to and telling of stories", // Favorite activity.
                    "Sure now I’m the kind of ghoul who’d rather be seen than heard, still I’m working hard not to fade into the background. I like sweaters, especially cardigans, and skirts in green", // Killer style.
                    "", // Pet.
                    "When other monster try to mimic my accent. No, I don’t want a charm. I don’t care how lucky or frosted it is", // Pet peeve.
                    "I’ve got a way of saying things other monsters misinterpret to mean that something bad is going to happen", // Monster quirk.
                    "Abbey Bominable, Clawdeen Wolf, Frankie Stein, Draculaura and Ghoulia Yelps" // Friends.
                ),
                new Personajes
                (
                    "Sirena", // Resource.
                    "Sirena Von Boo", // Name.
                    "Daughter of a ghost (father) and a mermaid (mother)", // Family.
                    "17", // Age.
                    "", // Description.
                    "Seaweed sushi. It’s so light and deadliciously tasty", // Favorite food.
                    "I’m super curious, so I love to treasure hunt in the sea and go to antique stores on land. I love scaring up unique and special freaky findings", // Favorite activity.
                    "I like to combine fashions from both sides of my scaritage, like intertwining strings of pearls with my chains. I mean if you’re going to rattle around, you might as well be fashionable while you’re doing so", // Killer style.
                    "I dream about having a pet one day, but so far that dream has not come true", // Pet.
                    "I hate being anchored to one spot. I’d much rather drift to wherever the tide takes me", // Pet peeve.
                    "I guess some monsters think I’m a bit of an air fin, but what I actually am is a daydreamer. I’m always floating away in my thoughts, on land or sea", // Monster quirk.
                    "Avea Trotter, Bonita Femur and Neighthan Rot" // Friends.
                ),
                new Personajes
                (
                    "Skelita", // Resource.
                    "Skelita Calaveras", // Name.
                    "Daughter of Los Eskeletos", // Family.
                    "15", // Age.
                    "", // Description.
                    "Sugar skulls", // Favorite food.
                    "I love doing anything associated with Dia de los Muertos, like face painting, sewing, creating dioramas, dancing, telling stories and most especially, spending time with mi familia", // Favorite activity.
                    "I’m proud of my heritage, but I also embrace my own individuality, so I mix traditional clothing with modern fashion to make a unique look of my very own. It may seem like I’m always ready to attend a party, but that’s what unlife is to me; a grand excuse to celebrate and have a good time", // Killer style.
                    "I have millions of pets and I get to see them each winter when they migrate to Hexico. They are the Monarch mariposas, and I love to be surrounded by their soft, fluttering beauty", // Pet.
                    "Headstones that are ignored and not well tended. It’s like forgetting who you were and where you came from", // Pet peeve.
                    "I get these feelings in my bones that something epic is about to happen. Unfortunately, I have no way of knowing just when the event will occur", // Monster quirk.
                    "Clawdeen Wolf, Jinafire Long, Frankie Stein and Ghoulia Yelps" // Friends.
                ),
                new Personajes
                (
                    "Skyra", // Resource.
                    "Skyra Bouncegait", // Name.
                    "A ghost", // Family.
                    "16", // Age.
                    "I’ve been accused of being “perky” but I prefer to think of myself as energetic, and it’s a good thing too, because it takes a lot of energy to cheer on the herd and encourage them to…Go! Fight! Win!", // Description.
                    "", // Favorite food.
                    "I am all about cheerleading! What other activity lets you socialize, exercise, and spur on your beasties all at the same time?!", // Favorite activity.
                    "School colors, school logos, school fashion! I don’t just have school spirit, I *am* a school spirit!", // Killer style.
                    "", // Pet.
                    "When monsters don’t at least give things the ol’ corral try", // Pet peeve.
                    "I don’t have a subtle bone in my body. I’m part ghost, so even my emotions are transparent", // Monster quirk.
                    "Fawntine Fallowheart and Penepole Steamtail" // Friends.
                ),
                new Personajes
                (
                    "SloMo", // Resource.
                    "Sloman “Slo Mo” Mortavitch", // Name.
                    "Son of the Zombies", // Family.
                    "16", // Age.
                    "", // Description.
                    "Oatsqueal. I think it is the perfect choice", // Favorite food.
                    "I have two favorites: casketball and chess. I cannot decide which one I like better, and I am in no hurry to choose", // Favorite activity.
                    "Unlife is filled with decisions, and I prefer not to waste brainpower on what I should wear. So I like to keep style decisions simple: jeans, T-shirt, sneakers and my monster letter jacket. This leaves me more time for important decisions like where to take Ghoulia on a date", // Killer style.
                    "I definitely need to do more research on this before I make a choice", // Pet.
                    "When monsters do not get my sense of humor. I hate having to explain the punch lines since that sucks the funny right out of the joke", // Pet peeve.
                    "It takes me a long time to make up my mind about anything. I need to research everything and have all the facts in front of me before I can say “yes” or “no.” I am not sure if that is a zombie thing or a “me” thing, but Ghoulia tells me I make decisions slower than a glacier moves", // Monster quirk.
                    "Ghoulia Yelps and Deuce Gorgon" // Friends.
                ),
                new Personajes
                (
                    "Spectra", // Resource.
                    "Spectra Vondergeist", // Name.
                    "Daughter of The Ghosts", // Family.
                    "16", // Age.
                    "", // Description.
                    "Angel food cake. It’s light and full of sweetness, much like me", // Favorite food.
                    "Providing certain information not readily available to the general Monster High student body", // Favorite activity.
                    "Silk, silk and more silk, accentuated with just a touch of metal. It allows me to freely float about with just a hint of rattle. It’s quite the haunting look, wouldn’t you agree?", // Killer style.
                    "Rhuen™ is my ghost ferret. Did you know that the name ferret comes from the Latin furittus, which means “little thief”? Of course, you didn’t until I told you!", // Pet.
                    "When certain monsters doubt the behind-the- screams information I have about what really happens at Monster High. It can be very difficult being the only one in the know", // Pet peeve.
                    "Well I’ve never really given this much thought you see since I find that it’s unhealthy to focus on what one does wrong, especially when contrasted with all the good one does in unlife. I am also not a gossip, despite rumors to the contrary", // Monster quirk.
                    "Twyla, Clawdeen Wolf, Clawd Wolf, Ghoulia Yelps and Catrine Demew" // Friends.
                ),
                new Personajes
                (
                    "Toralei", // Resource.
                    "Toralei Stripe", // Name.
                    "Daughter of The Werecat", // Family.
                    "15, but I’m still on the first of my nine lives", // Age.
                    "", // Description.
                    "Milkshakes and anchovies—separately, not mixed together", // Favorite food.
                    "It’s either taking a nap or waking up from a nap and immediately taking another one", // Favorite activity.
                    "I purrfer fashions that accentuate my natural feline grace, while adding just enough spikiness in my accessories to say, “I don’t come when I’m called”", // Killer style.
                    "Sweet Fang™ is my pet saber-tooth tiger cub. She’s much more cuddly than I am", // Pet.
                    "I don’t like being rubbed the wrong way", // Pet peeve.
                    "Purrhaps I could be faulted for my fascination with the claw of cause and effect…or not. I suppose it all depends on whether or not you’re the monster being affected", // Monster quirk.
                    "Purrsephone, Meowlody, Frankie Stein and Spectra Vondergeist" // Friends.
                ),
                new Personajes
                (
                    "Twyla", // Resource.
                    "Twyla", // Name.
                    "Daughter of The Boogey Man", // Family.
                    "15", // Age.
                    "", // Description.
                    "Nightmares, of course", // Favorite food.
                    "I love capturing normie nightmares so only the things that make sweet dreams get through. How can you disagree that’s not a good thing?", // Favorite activity.
                    "My friends tell me that I have a shadowy figure or maybe it’s that I am a shadowy figure…anyways, I do love dark blues and deep purples, especially when they blend together like smoke on the water", // Killer style.
                    "A dust bunny named Dustin™. I found the wind blowing him one day, so I rescued and adopted him", // Pet.
                    "When other monsters don’t understand why I would want to help normies, and normies don’t understand I’m not there to scare them. It’s really hard to build dreams on suspicious minds. Oh, and vacuum cleaners", // Pet peeve.
                    "I’m painfully shy, and I spend most of my time sleeping/hiding under beds so it’s hard to make new friends. Oh well, it’s just a day in the life of a boogey monster", // Monster quirk.
                    "Spectra Vondergeist, Howleen Wolf and Gigi Grant" // Friends.
                ),
                new Personajes
                (
                    "Vandala", // Resource.
                    "Vandala Doubloons", // Name.
                    "Daughter of A Pirate Ghost", // Family.
                    "16", // Age.
                    "", // Description.
                    "Fish and Crypts", // Favorite food.
                    "Exploring uncharted areas of the deep boo sea. Ye never know what new lands you’ll discover or what treasures you’ll dig up", // Favorite activity.
                    "Nautical Yo-Ho Bo-Ho Chic. Flowing silhouettes remind me of bow spray on the open sea and a fair wind in my sails", // Killer style.
                    "A cuttlefish with an eye patch. Aye, his name is Aye!", // Pet.
                    "When a ghost can't make a decision! A captain must be decisive, so either give me an order to carry out or give me the helm and let me steer", // Pet peeve.
                    "Arrgh, this is so embarrassing, but sometimes I get seasick... I'm a ghost – how does that even happen? Also, sometimes the lure of adventure capsizes my ability to consider the consequences", // Monster quirk.
                    "Kiyomi Haunterly" // Friends.
                ),
                new Personajes
                (
                    "Venus", // Resource.
                    "Venus Mcflytrap", // Name.
                    "Daughter of The Plant Monster", // Family.
                    "15", // Age.
                    "", // Description.
                    "Fresh water and sunshine", // Favorite food.
                    "I like hiking, camping and convincing other monsters how important it is to be caretakers of the world that we live in", // Favorite activity.
                    "Bright, loud and in your face. The brightest flowers get the most attention, and I’m no shrinking violet when it comes to wanting monsters to look my way", // Killer style.
                    "A Venus flytrap called Chewlian™. He’s got a really snappy personality", // Pet.
                    "Monsters who trash the environment", // Pet peeve.
                    "Right, I’m passionate about protecting the world we live in, and I don’t want it trashed—that’s not my flaw—but sometimes I accidentally-on-purpose use my pollens of persuasion to get monsters to volunteer for my cause", // Monster quirk.
                    "Lagoona Blue, Draculaura and Ghoulia Yelps" // Friends.
                ),
                new Personajes
                (
                    "Viperine", // Resource.
                    "Viperine Gorgon", // Name.
                    "Daughter of Stheno(steth-an-uh) – Sister of Medusa", // Family.
                    "17", // Age.
                    "", // Description.
                    "Dates…stuffed with almonds and wrapped in bacon. Delicioussss…", // Favorite food.
                    "I love doing, shopping for and experimenting with makeup. I do my friends makeup for dances and school photos and sometimes I go to the elder monsters’ home and do makeovers for the monsters that live there. They have great stories to tell, and I get to hear them. #WinWin", // Favorite activity.
                    "Hippie Boho Chic. I like to be comfortable AND I never like scaling back when it comes to mixing different colors, patterns and fabrics together", // Killer style.
                    "In case you haven’t noticed, under this mane of freaky fabulous hair is a rather active nest of vipers. I really don’t have room in my unlife for anything else", // Pet.
                    "Cheap makeup. Might as well just use finger paint", // Pet peeve.
                    "Some people wave, some high five, some hug… I bite. I just can’t help myself. It’s the way I show affection. I try to remember to just shake hands, but sometimes I forget", // Monster quirk.
                    "Elissabat and Honey Swamp" // Friends.
                ),
                new Personajes
                (
                    "Wydowna", // Resource.
                    "Wydowna Spider", // Name.
                    "Daughter of Arachne", // Family.
                    "16", // Age.
                    "", // Description.
                    "Fonboo is my fave because with six arms I can do sweet and savory at the same time", // Favorite food.
                    "I love lending helping hands to my friends by taking their ideas and bringing them to unlife with my illustration and sewing skills. Since I’m really a fan ghoul at hearts, it’s scary cool that they trust me with their visions", // Favorite activity.
                    "I like to call my style rock ‘n’ roll geek. I love to get wrapped up in spikes, studs and colors that totally scream. It may look intimidating but my hearts are as soft as silk", // Killer style.
                    "Shoo is my pet fly. She’s always supplying me with buzz-worthy moments", // Pet.
                    "Can every monster just stop with the black widow thing? It’s just as unfunny the thousandth time as it was the first, and it really gets my web in a tangle", // Pet peeve.
                    "I’m such a multi-tasker that sometimes I can get totally burned out. I don’t even realize it until I find myself curled up into a ball wondering how I got so tired ''all of a sudden''", // Monster quirk.
                    "Ghoulia Yelps and Clawdeen Wolf" // Friends.
                ),
                /*new Personajes
                (
                    "", // Resource.
                    "", // Resource.
                    "", // Name.
                    "", // Family.
                    "", // Age.
                    "", // Description.
                    "", // Favorite food.
                    "", // Favorite activity.
                    "", // Killer style.
                    "", // Pet.
                    "", // Pet peeve.
                    "", // Monster quirk.
                    "" // Friends.
                ),*/
            };
        }

        internal readonly string Texto_Título = "Monster High Characters by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        //internal static Stopwatch FPS_Cronómetro = Stopwatch.StartNew();
        internal long FPS_Segundo_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        internal bool Ocupado = false;

        internal float Variable_Zoom = 1f;
        internal static bool Variable_Filtro_Negativo = false;
        internal static bool Variable_Filtro_Raíz_Cuadrada = false;
        internal static bool Variable_Filtro_Logaritmo = false;
        internal static byte[] Variable_Matriz_Bytes_Filtro = null;

        private void Ventana_Monster_High_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Characters known: " + Program.Traducir_Número(Personajes.Matriz_Personajes.Length) + ", also check the Monster High resource pack]";
                this.WindowState = FormWindowState.Maximized;
                foreach (Personajes Personaje in Personajes.Matriz_Personajes)
                {
                    ComboBox_Personaje.Items.Add(Personaje.Name);
                }
                ComboBox_Personaje.SelectedIndex = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Monster_High_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Monster_High_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Monster_High_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Monster_High_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                Picture_Header.SizeMode = Menú_Contextual_Ajustar_Imágenes.Checked && Picture_Header.ClientSize.Width < 978 ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage;
                Picture_Hero.SizeMode = Menú_Contextual_Ajustar_Imágenes.Checked && Picture_Hero.ClientSize.Height < 720 ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Monster_High_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        this.Close();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Personaje_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Personaje.SelectedIndex > -1)
                {
                    Personajes Personaje = Personajes.Matriz_Personajes[ComboBox_Personaje.SelectedIndex];
                    Picture_Thumb.Image = Program.Obtener_Imagen_Recursos_Externos(Application.StartupPath + "\\Secrets\\Images\\Monster_High_Thumb_" + Personaje.Resource, CheckState.Checked);
                    Picture_Grid.Image = Program.Obtener_Imagen_Recursos_Externos(Application.StartupPath + "\\Secrets\\Images\\Monster_High_Grid_" + Personaje.Resource, CheckState.Checked);
                    Picture_Header.Image = Program.Obtener_Imagen_Recursos_Externos(Application.StartupPath + "\\Secrets\\Images\\Monster_High_Header_" + Personaje.Resource, CheckState.Checked);
                    Picture_Hero.Image = Program.Obtener_Imagen_Recursos_Externos(Application.StartupPath + "\\Secrets\\Images\\Monster_High_Hero_" + Personaje.Resource, CheckState.Indeterminate);
                    string Texto_Biografía = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang3082{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}{\\f1\\fnil\\fcharset0 Calibri;}}\r\n{\\*\\generator Riched20 6.3.9600}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs" + (10 * 2).ToString() + " ";
                    Texto_Biografía += "\\ul \\b [Name]\\b0 \\ulnone \\par " + (!string.IsNullOrEmpty(Personaje.Name) ? Personaje.Name.TrimEnd(".".ToCharArray()) : "?") + ".\\par \\par ";
                    Texto_Biografía += "\\ul \\b [Family]\\b0 \\ulnone \\par " + (!string.IsNullOrEmpty(Personaje.Family) ? Personaje.Family.TrimEnd(".".ToCharArray()) : "?") + ".\\par \\par ";
                    Texto_Biografía += "\\ul \\b [Age]\\b0 \\ulnone \\par " + (!string.IsNullOrEmpty(Personaje.Age) ? Personaje.Age.TrimEnd(".".ToCharArray()) : "?") + ".\\par \\par ";
                    Texto_Biografía += "\\ul \\b [Description]\\b0 \\ulnone \\par " + (!string.IsNullOrEmpty(Personaje.Description) ? Personaje.Description.TrimEnd(".".ToCharArray()) : "?") + ".\\par \\par ";
                    Texto_Biografía += "\\ul \\b [Favorite Food]\\b0 \\ulnone \\par " + (!string.IsNullOrEmpty(Personaje.Favorite_Food) ? Personaje.Favorite_Food.TrimEnd(".".ToCharArray()) : "?") + ".\\par \\par ";
                    Texto_Biografía += "\\ul \\b [Favorite Activity]\\b0 \\ulnone \\par " + (!string.IsNullOrEmpty(Personaje.Favorite_Activity) ? Personaje.Favorite_Activity.TrimEnd(".".ToCharArray()) : "?") + ".\\par \\par ";
                    Texto_Biografía += "\\ul \\b [Killer Style]\\b0 \\ulnone \\par " + (!string.IsNullOrEmpty(Personaje.Killer_Style) ? Personaje.Killer_Style.TrimEnd(".".ToCharArray()) : "?") + ".\\par \\par ";
                    Texto_Biografía += "\\ul \\b [Pet]\\b0 \\ulnone \\par " + (!string.IsNullOrEmpty(Personaje.Pet) ? Personaje.Pet.TrimEnd(".".ToCharArray()) : "?") + ".\\par \\par ";
                    Texto_Biografía += "\\ul \\b [Pet Peeve]\\b0 \\ulnone \\par " + (!string.IsNullOrEmpty(Personaje.Pet_Peeve) ? Personaje.Pet_Peeve.TrimEnd(".".ToCharArray()) : "?") + ".\\par \\par ";
                    Texto_Biografía += "\\ul \\b [Monster Quirk]\\b0 \\ulnone \\par " + (!string.IsNullOrEmpty(Personaje.Monster_Quirk) ? Personaje.Monster_Quirk.TrimEnd(".".ToCharArray()) : "?") + ".\\par \\par ";
                    Texto_Biografía += "\\ul \\b [Friends]\\b0 \\ulnone \\par " + (!string.IsNullOrEmpty(Personaje.Friends) ? Personaje.Friends.TrimEnd(".".ToCharArray()) : "?") + ".\\par ";
                    Texto_Biografía += "\\pard\\sa200\\sl276\\slmult1\\f1\\fs22\\lang10\\par}";
                    float Zoom = Variable_Zoom;
                    RichTextBox_Personaje.Rtf = Texto_Biografía.Replace("!.", "!").Replace("?.", "?").Replace("… ", "…").Replace("…", "... ").Replace("©", null).Replace("®", null).Replace("™", null);
                    RichTextBox_Personaje.ZoomFactor = Zoom != 1.5f ? 1.5f : 2.5f;
                    RichTextBox_Personaje.ZoomFactor = Zoom;
                    Picture_Thumb.Refresh();
                    Picture_Grid.Refresh();
                    Picture_Header.Refresh();
                    Picture_Hero.Refresh();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Personaje_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    if (ComboBox_Personaje.SelectedIndex > -1)
                    {
                        Clipboard.SetText(ComboBox_Personaje.Text);
                        SystemSounds.Asterisk.Play();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Pictures_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right)
                {
                    ComboBox_Personaje.Select();
                    ComboBox_Personaje.Focus();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                //Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Monster_High;
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Acerca_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Acerca Ventana = new Ventana_Acerca();
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Depurador_Excepciones_Click(object sender, EventArgs e)
        {
            try
            {
                Variable_Excepción = false;
                Variable_Excepción_Imagen = false;
                Variable_Excepción_Total = 0;
                /*Barra_Estado_Botón_Excepción.Visible = false;
                Barra_Estado_Separador_1.Visible = false;
                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                Barra_Estado_Botón_Excepción.Text = "Exceptions: 0";*/
                Ventana_Depurador_Excepciones Ventana = new Ventana_Depurador_Excepciones();
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Abrir_Carpeta_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Visor_Cuadros);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Visor_Cuadros, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // ...
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Filtro_Negativo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Filtro_Negativo = Menú_Contextual_Filtro_Negativo.Checked;
                Variable_Matriz_Bytes_Filtro = Program.Combinar_Matrices_Bytes_Filtros(new byte[][]
                {
                    Variable_Filtro_Negativo ? Program.Matriz_Bytes_Filtro_Negativo : null,
                    Variable_Filtro_Raíz_Cuadrada ? Program.Matriz_Bytes_Filtro_Raíz_Cuadrada : null,
                    Variable_Filtro_Logaritmo ? Program.Matriz_Bytes_Filtro_Logaritmo : null
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Filtro_Raíz_Cuadrada_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Filtro_Raíz_Cuadrada = Menú_Contextual_Filtro_Raíz_Cuadrada.Checked;
                Variable_Matriz_Bytes_Filtro = Program.Combinar_Matrices_Bytes_Filtros(new byte[][]
                {
                    Variable_Filtro_Negativo ? Program.Matriz_Bytes_Filtro_Negativo : null,
                    Variable_Filtro_Raíz_Cuadrada ? Program.Matriz_Bytes_Filtro_Raíz_Cuadrada : null,
                    Variable_Filtro_Logaritmo ? Program.Matriz_Bytes_Filtro_Logaritmo : null
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Filtro_Logaritmo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Filtro_Logaritmo = Menú_Contextual_Filtro_Logaritmo.Checked;
                Variable_Matriz_Bytes_Filtro = Program.Combinar_Matrices_Bytes_Filtros(new byte[][]
                {
                    Variable_Filtro_Negativo ? Program.Matriz_Bytes_Filtro_Negativo : null,
                    Variable_Filtro_Raíz_Cuadrada ? Program.Matriz_Bytes_Filtro_Raíz_Cuadrada : null,
                    Variable_Filtro_Logaritmo ? Program.Matriz_Bytes_Filtro_Logaritmo : null
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG_Click(object sender, EventArgs e)
        {
            try
            {
                Exportar_Pack_Recursos_Cuadros(3, true, 95);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG_Click(object sender, EventArgs e)
        {
            try
            {
                Exportar_Pack_Recursos_Cuadros(3, false, 0);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG_Click(object sender, EventArgs e)
        {
            try
            {
                Exportar_Pack_Recursos_Cuadros(4, true, 95);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG_Click(object sender, EventArgs e)
        {
            try
            {
                Exportar_Pack_Recursos_Cuadros(4, false, 0);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Ajustar_Imágenes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Menú_Contextual_Ajustar_Imágenes.Checked)
                {
                    Picture_Header.SizeMode = PictureBoxSizeMode.CenterImage;
                    Picture_Hero.SizeMode = PictureBoxSizeMode.Normal;
                }
                else
                {
                    Picture_Header.SizeMode = Picture_Header.ClientSize.Width >= 978 ? PictureBoxSizeMode.CenterImage : PictureBoxSizeMode.Zoom;
                    Picture_Hero.SizeMode = Picture_Hero.ClientSize.Height >= 720 ? PictureBoxSizeMode.Normal : PictureBoxSizeMode.Zoom;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Generates a new resource pack for the specified pack format containing the real HD paintings in a 8.192 x 8.192 texture. Note: the pack number will be the only difference in the pack, so it should load correctly on any Minecraft version (in theory).
        /// </summary>
        /// <param name="Pack">A number between 1 and 4 (Minecraft 1.13+).</param>
        internal void Exportar_Pack_Recursos_Cuadros(int Pack, bool Exportar_JPEG, int Calidad_JPEG)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Visor_Cuadros);
                if (Directory.Exists(Program.Ruta_Guardado_Imágenes_Visor_Cuadros))
                {
                    Bitmap Imagen = new Bitmap(8192, 8192, PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    Pintar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    SolidBrush Pincel_Fondo = new SolidBrush(Color.FromArgb(214, 127, 255));
                    SolidBrush Pincel_Bordes = new SolidBrush(Color.FromArgb(107, 63, 127));
                    for (int Índice_Y = 0; Índice_Y < 8192; Índice_Y += 512)
                    {
                        for (int Índice_X = 0; Índice_X < 8192; Índice_X += 512)
                        {
                            Pintar.FillRectangle(Pincel_Bordes, Índice_X, Índice_Y, 512, 512);
                            Pintar.FillRectangle(Pincel_Fondo, Índice_X + 1, Índice_Y + 1, 510, 510);
                        }
                    }
                    Pincel_Fondo.Dispose();
                    Pincel_Bordes.Dispose();
                    Pincel_Fondo = null;
                    Pincel_Bordes = null;

                    // Draw the background texture of the paintings, here like the original.
                    TextureBrush Pincel_Madera = new TextureBrush(Resources.Cuadros_Madera, WrapMode.Tile);
                    Pintar.FillRectangle(Pincel_Madera, 6144, 0, 2048, 2048);
                    Pincel_Madera.Dispose();
                    Pincel_Madera = null;

                    // Draw the real HD paintings.
                    /*string Ruta_Cuadros = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "\\__Monster High\\Paintings";

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Kebab.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(0, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Aztec.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(512, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Alban.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(1024, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Aztec2.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(1536, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Bomb.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(2048, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Plant.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(2560, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Wasteland.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(3072, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Pool.png", CheckState.Unchecked), 1024, 512, true, true, CheckState.Unchecked), new Rectangle(0, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Courbet.png", CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(1024, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Sea.png", CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(2048, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Sunset.png", CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(3072, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Sea.png", CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(4096, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Wanderer.png", CheckState.Unchecked), 512, 1024, false, true, CheckState.Unchecked), new Rectangle(0, 2048, 512, 1024), new Rectangle(0, 0, 512, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Graham.png", CheckState.Unchecked), 512, 1024, false, true, CheckState.Unchecked), new Rectangle(512, 2048, 512, 1024), new Rectangle(0, 0, 512, 1024), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Fighters.png", CheckState.Unchecked), 2048, 1024, false, true, CheckState.Unchecked), new Rectangle(0, 3072, 2048, 1024), new Rectangle(0, 0, 2048, 1024), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Match.png", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(0, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Bust.png", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(1024, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Stage.png", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(2048, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Void.png", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(3072, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_SkullAndRoses.png", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(4096, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Wither.png", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(5120, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Pointer.png", CheckState.Unchecked), 2048, 2048, false, true, CheckState.Unchecked), new Rectangle(0, 6144, 2048, 2048), new Rectangle(0, 0, 2048, 2048), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Pigscene.png", CheckState.Unchecked), 2048, 2048, false, true, CheckState.Unchecked), new Rectangle(2048, 6144, 2048, 2048), new Rectangle(0, 0, 2048, 2048), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_BurningSkull.png", CheckState.Unchecked), 2048, 2048, false, true, CheckState.Unchecked), new Rectangle(4096, 6144, 2048, 2048), new Rectangle(0, 0, 2048, 2048), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Skeleton.png", CheckState.Unchecked), 2048, 1536, false, true, CheckState.Unchecked), new Rectangle(6144, 2048, 2048, 1536), new Rectangle(0, 0, 2048, 1536), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_DonkeyKong.png", CheckState.Unchecked), 2048, 1536, false, true, CheckState.Unchecked), new Rectangle(6144, 3584, 2048, 1536), new Rectangle(0, 0, 2048, 1536), GraphicsUnit.Pixel);*/

                    // Do the same but from the resources (the images won't be perfectly centered).
                    string Ruta_Recursos_Externos_Paintings_MH = Application.StartupPath + "\\Secrets\\Images";

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_17_52_22_980", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(0, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_19_00_04_160", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(512, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_18_19_07_854", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(1024, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_23_21_34_24_943", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(1536, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_23_21_59_31_772", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(2048, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_18_15_28_995", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(2560, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_23_22_00_00_371", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(3072, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_23_21_45_34_974", CheckState.Unchecked), 1024, 512, true, true, CheckState.Unchecked), new Rectangle(0, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_18_28_47_048", CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(1024, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_18_31_12_583", CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(2048, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_18_43_25_456", CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(3072, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_19_05_44_414", CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(4096, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_18_07_00_468", CheckState.Unchecked), 512, 1024, false, true, CheckState.Unchecked), new Rectangle(0, 2048, 512, 1024), new Rectangle(0, 0, 512, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_18_05_38_625", CheckState.Unchecked), 512, 1024, false, true, CheckState.Unchecked), new Rectangle(512, 2048, 512, 1024), new Rectangle(0, 0, 512, 1024), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_18_51_09_261", CheckState.Unchecked), 2048, 1024, false, true, CheckState.Unchecked), new Rectangle(0, 3072, 2048, 1024), new Rectangle(0, 0, 2048, 1024), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_17_47_05_408", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(0, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_18_13_30_001", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(1024, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_18_23_18_261", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(2048, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_18_16_45_074", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(3072, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_17_48_56_235", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(4096, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_18_03_17_290", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(5120, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_17_45_39_805", CheckState.Unchecked), 2048, 2048, false, true, CheckState.Unchecked), new Rectangle(0, 6144, 2048, 2048), new Rectangle(0, 0, 2048, 2048), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_17_41_28_148", CheckState.Unchecked), 2048, 2048, false, true, CheckState.Unchecked), new Rectangle(2048, 6144, 2048, 2048), new Rectangle(0, 0, 2048, 2048), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_17_47_51_606", CheckState.Unchecked), 2048, 2048, false, true, CheckState.Unchecked), new Rectangle(4096, 6144, 2048, 2048), new Rectangle(0, 0, 2048, 2048), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_19_16_52_958", CheckState.Unchecked), 2048, 1536, false, true, CheckState.Unchecked), new Rectangle(6144, 2048, 2048, 1536), new Rectangle(0, 0, 2048, 1536), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\2018_10_21_18_09_45_533", CheckState.Unchecked), 2048, 1536, false, true, CheckState.Unchecked), new Rectangle(6144, 3584, 2048, 1536), new Rectangle(0, 0, 2048, 1536), GraphicsUnit.Pixel);

                    Pintar.Dispose();
                    Pintar = null;

                    // Allow even post support to all the available image filters.
                    if (Variable_Filtro_Negativo || Variable_Filtro_Raíz_Cuadrada || Variable_Filtro_Logaritmo)
                    {
                        Imagen = Program.Obtener_Imagen_Filtrada(Imagen, Variable_Matriz_Bytes_Filtro);
                    }

                    // Start a new ZIP file to store the resource pack.
                    string Ruta = Program.Ruta_Guardado_Imágenes_Visor_Cuadros + "\\MH Paintings [" + (Pack < 4 ? "1.12.2-" : "1.13+") + "] [" + (!Exportar_JPEG ? "PNG" : "JPEG") + "] " + Program.Obtener_Nombre_Temporal() + ".zip";
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    ICSharpCode.SharpZipLib.Zip.ZipFile Archivo_ZIP = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(Lector);

                    // Write the "pack.mcmeta".
                    string Ruta_Pack_MCMETA = Program.Ruta_Guardado_Imágenes_Visor_Cuadros + "\\" + Program.Obtener_Nombre_Temporal() + " pack.mcmeta";
                    FileStream Lector_Pack_MCMETA = new FileStream(Ruta_Pack_MCMETA, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector_Pack_MCMETA.SetLength(0L);
                    Lector_Pack_MCMETA.Seek(0L, SeekOrigin.Begin);
                    StreamWriter Lector_Texto_Pack_MCMETA = new StreamWriter(Lector_Pack_MCMETA, Encoding.UTF8);
                    Lector_Texto_Pack_MCMETA.WriteLine("{");
                    Lector_Texto_Pack_MCMETA.WriteLine("  \"pack\": {");
                    Lector_Texto_Pack_MCMETA.WriteLine("    \"pack_format\": " + Pack.ToString() + ",");
                    Lector_Texto_Pack_MCMETA.WriteLine("    \"description\": \"§fMH Paintings§r for §fJava Edition\\n§6Author:§r §cJupisoft\"");
                    Lector_Texto_Pack_MCMETA.WriteLine("  }");
                    Lector_Texto_Pack_MCMETA.WriteLine("}");
                    Lector_Texto_Pack_MCMETA.Close();
                    Lector_Texto_Pack_MCMETA.Dispose();
                    Lector_Texto_Pack_MCMETA = null;
                    Lector_Pack_MCMETA.Close();
                    Lector_Pack_MCMETA.Dispose();
                    Lector_Pack_MCMETA = null;

                    // Write the "pack.png".
                    string Ruta_Pack_PNG = Program.Ruta_Guardado_Imágenes_Visor_Cuadros + "\\" + Program.Obtener_Nombre_Temporal() + " pack.png";
                    Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings_MH + "\\pack", CheckState.Unchecked).Save(Ruta_Pack_PNG, ImageFormat.Png); // Jupisoft_256

                    // Write the "paintings_kristoffer_zetterstrand.png".
                    string Ruta_Paintings_Kristoffer_Zetterstrand_PNG = Program.Ruta_Guardado_Imágenes_Visor_Cuadros + "\\" + Program.Obtener_Nombre_Temporal() + " paintings_kristoffer_zetterstrand" + (!Exportar_JPEG ? ".png" : ".jpg");
                    if (!Exportar_JPEG) // Save as a PNG image (~ 62 MB).
                    {
                        Imagen.Save(Ruta_Paintings_Kristoffer_Zetterstrand_PNG, ImageFormat.Png);
                    }
                    else // Save as a JPEG image (Quality 90: ~ 6 MB, Quality 100: ~ 18 MB).
                    {
                        ImageCodecInfo Codificador = Program.Obtener_Imagen_Codificador_Guid(ImageFormat.Jpeg.Guid);
                        if (Codificador != null) // We can choose any JPEG compression.
                        {
                            EncoderParameters Parámetros = new EncoderParameters(1);
                            Parámetros.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)Calidad_JPEG);
                            Imagen.Save(Ruta_Paintings_Kristoffer_Zetterstrand_PNG, Codificador, Parámetros);
                            Parámetros.Dispose();
                            Parámetros = null;
                            Codificador = null;
                        }
                        else Imagen.Save(Ruta_Paintings_Kristoffer_Zetterstrand_PNG, ImageFormat.Jpeg); // Default compression.
                    }

                    // Once all the files for the ZIP archive have been saved, add them to the ZIP itself.
                    Archivo_ZIP.BeginUpdate();
                    Archivo_ZIP.Add(Ruta_Pack_MCMETA, "pack.mcmeta");
                    Archivo_ZIP.Add(Ruta_Pack_PNG, "pack.png");
                    Archivo_ZIP.AddDirectory("assets\\minecraft\\textures\\painting");
                    Archivo_ZIP.Add(Ruta_Paintings_Kristoffer_Zetterstrand_PNG, "assets\\minecraft\\textures\\painting\\paintings_kristoffer_zetterstrand.png"); // Note: if the original image is a JPEG, it will have a false extension, but it should still work normally.
                    Archivo_ZIP.CommitUpdate();
                    Archivo_ZIP.Close();
                    Archivo_ZIP = null;
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    Imagen.Dispose();
                    Imagen = null;

                    // Tries to delete the files already added to the ZIP file.
                    Program.Eliminar_Archivo_Carpeta(Ruta_Pack_MCMETA);
                    Program.Eliminar_Archivo_Carpeta(Ruta_Pack_PNG);
                    Program.Eliminar_Archivo_Carpeta(Ruta_Paintings_Kristoffer_Zetterstrand_PNG);

                    // Done.
                    Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Visor_Cuadros, ProcessWindowStyle.Maximized);
                    SystemSounds.Asterisk.Play();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
