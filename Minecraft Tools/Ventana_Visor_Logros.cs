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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Visor_Logros : Form
    {
        public Ventana_Visor_Logros()
        {
            InitializeComponent();
        }

        // So far only the files ".minecraft\usercache.json" and ".minecraft\logs\latest.log"
        // keep the player names inside of them... the rest i've found so far were only the
        // UUID of the players and although this application can emulate the Java code to get
        // an UUID, it doesn't match with the ones supplied by Mojang when registering the
        // player names, so sadly I believe that those won't be decodable at all. Examples:
        // "[09:23:03] [main/INFO]: Setting user: Jupisoft".
        // "[09:24:20] [Server thread/INFO]: Jupisoft[...] logged in with entity id 139 at (-137.46153584685203, 68.0, -65.21694673353241)".
        // "[09:24:20] [Server thread/INFO]: Jupisoft joined the game".
        // "[09:24:21] [pool-3-thread-1/WARN]: ...[id=7af45d88-e129-4e09-b1f6-9eee3e636325,name=Jupisoft...]".
        // "[04:52:04] [User Authenticator #1/INFO]: UUID of player ISpectre23 is 04ac603c-fc4c-47fb-b1e8-e559f2c65176".
        // "[04:52:04] [Server thread/INFO]: ISpectre23[/25.10.64.110:60953] logged in with entity id 1618 at (-79.5, 71.0, -186.5)".
        // "[04:52:04] [Server thread/INFO]: ISpectre23 joined the game".
        // "[04:52:05] [Render thread/INFO]: [CHAT] ISpectre23 joined the game".

        /// <summary>
        /// Structure that holds up all the information about a Hermit.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Logros_Minecraft
        {
            internal string Ruta_Recurso;
            internal string Categoría;
            internal string Marco;
            internal bool Oculto;
            internal string Título;
            internal string Descripción;

            internal Logros_Minecraft(string Ruta_Recurso, string Categoría, string Marco, bool Oculto, string Título, string Descripción)
            {
                this.Ruta_Recurso = Ruta_Recurso;
                this.Categoría = Categoría;
                this.Marco = Marco;
                this.Oculto = Oculto;
                this.Título = Título;
                this.Descripción = Descripción;
            }

            /// <summary>
            /// Array with all the Minecraft advancements from the snapshot 20w06a (Minecraft 1.16).
            /// </summary>
            internal static readonly Logros_Minecraft[] Matriz_Logros_Minecraft = new Logros_Minecraft[]
            {
                // Adventure advancements:
                new Logros_Minecraft
                (
                    "root",
                    "adventure",
                    "task",
                    false,
                    "Adventure",
                    "Adventure, exploration and combat"
                ),
                new Logros_Minecraft
                (
                    "adventuring_time",
                    "adventure",
                    "challenge",
                    false,
                    "Adventuring Time",
                    "Discover every biome"
                ),
                new Logros_Minecraft
                (
                    "arbalistic",
                    "adventure",
                    "challenge",
                    true,
                    "Arbalistic",
                    "Kill five unique mobs with one crossbow shot"
                ),
                new Logros_Minecraft
                (
                    "hero_of_the_village",
                    "adventure",
                    "challenge",
                    true,
                    "Hero of the Village",
                    "Successfully defend a village from a raid"
                ),
                new Logros_Minecraft
                (
                    "honey_block_slide",
                    "adventure",
                    "task",
                    false,
                    "Sticky Situation",
                    "Jump into a Honey Block to break your fall"
                ),
                new Logros_Minecraft
                (
                    "kill_a_mob",
                    "adventure",
                    "task",
                    false,
                    "Monster Hunter",
                    "Kill any hostile monster"
                ),
                new Logros_Minecraft
                (
                    "kill_all_mobs",
                    "adventure",
                    "challenge",
                    false,
                    "Monsters Hunted",
                    "Kill one of every hostile monster"
                ),
                new Logros_Minecraft
                (
                    "ol_betsy",
                    "adventure",
                    "task",
                    false,
                    "Ol' Betsy",
                    "Shoot a crossbow"
                ),
                new Logros_Minecraft
                (
                    "shoot_arrow",
                    "adventure",
                    "task",
                    false,
                    "Take Aim",
                    "Shoot something with an arrow"
                ),
                new Logros_Minecraft
                (
                    "sleep_in_bed",
                    "adventure",
                    "task",
                    false,
                    "Sweet Dreams",
                    "Change your respawn point"
                ),
                new Logros_Minecraft
                (
                    "sniper_duel",
                    "adventure",
                    "challenge",
                    false,
                    "Sniper Duel",
                    "Kill a Skeleton from at least 50 meters away"
                ),
                new Logros_Minecraft
                (
                    "summon_iron_golem",
                    "adventure",
                    "goal",
                    false,
                    "Hired Help",
                    "Summon an Iron Golem to help defend a village"
                ),
                new Logros_Minecraft
                (
                    "throw_trident",
                    "adventure",
                    "task",
                    false,
                    "A Throwaway Joke",
                    "Throw a trident at something. Note: Throwing away your only weapon is not a good idea."
                ),
                new Logros_Minecraft
                (
                    "totem_of_undying",
                    "adventure",
                    "goal",
                    false,
                    "Postmortal",
                    "Use a Totem of Undying to cheat death"
                ),
                new Logros_Minecraft
                (
                    "trade",
                    "adventure",
                    "task",
                    false,
                    "What a Deal!",
                    "Successfully trade with a Villager"
                ),
                new Logros_Minecraft
                (
                    "two_birds_one_arrow",
                    "adventure",
                    "challenge",
                    false,
                    "Two Birds, One Arrow",
                    "Kill two Phantoms with a piercing arrow"
                ),
                new Logros_Minecraft
                (
                    "very_very_frightening",
                    "adventure",
                    "task",
                    false,
                    "Very Very Frightening",
                    "Strike a Villager with lightning"
                ),
                new Logros_Minecraft
                (
                    "voluntary_exile",
                    "adventure",
                    "task",
                    true,
                    "Voluntary Exile",
                    "Kill a raid captain. Maybe consider staying away from villages for the time being..."
                ),
                new Logros_Minecraft
                (
                    "whos_the_pillager_now",
                    "adventure",
                    "task",
                    false,
                    "Who's the Pillager Now?",
                    "Give a Pillager a taste of their own medicine"
                ),

                // End advancements:
                new Logros_Minecraft
                (
                    "root",
                    "end",
                    "task",
                    false,
                    "The End",
                    "Or the beginning?"
                ),
                new Logros_Minecraft
                (
                    "dragon_breath",
                    "end",
                    "goal",
                    false,
                    "You Need a Mint",
                    "Collect dragon's breath in a glass bottle"
                ),
                new Logros_Minecraft
                (
                    "dragon_egg",
                    "end",
                    "goal",
                    false,
                    "The Next Generation",
                    "Hold the Dragon Egg"
                ),
                new Logros_Minecraft
                (
                    "elytra",
                    "end",
                    "goal",
                    false,
                    "Sky's the Limit",
                    "Find elytra"
                ),
                new Logros_Minecraft
                (
                    "enter_end_gateway",
                    "end",
                    "task",
                    false,
                    "Remote Getaway",
                    "Escape the island"
                ),
                new Logros_Minecraft
                (
                    "find_end_city",
                    "end",
                    "task",
                    false,
                    "The City at the End of the Game",
                    "Go on in, what could happen?"
                ),
                new Logros_Minecraft
                (
                    "kill_dragon",
                    "end",
                    "task",
                    false,
                    "Free the End",
                    "Good luck"
                ),
                new Logros_Minecraft
                (
                    "levitate",
                    "end",
                    "challenge",
                    false,
                    "Great View From Up Here",
                    "Levitate up 50 blocks from the attacks of a Shulker"
                ),
                new Logros_Minecraft
                (
                    "respawn_dragon",
                    "end",
                    "goal",
                    false,
                    "The End... Again...",
                    "Respawn the Ender Dragon"
                ),

                // Husbandry advancements:
                new Logros_Minecraft
                (
                    "root",
                    "husbandry",
                    "task",
                    false,
                    "Husbandry",
                    "The world is full of friends and food"
                ),
                new Logros_Minecraft
                (
                    "balanced_diet",
                    "husbandry",
                    "challenge",
                    false,
                    "A Balanced Diet",
                    "Eat everything that is edible, even if it's not good for you"
                ),
                new Logros_Minecraft
                (
                    "break_diamond_hoe",
                    "husbandry",
                    "challenge",
                    false,
                    "Serious Dedication",
                    "Completely use up a diamond hoe, and then reevaluate your life choices"
                ),
                new Logros_Minecraft
                (
                    "bred_all_animals",
                    "husbandry",
                    "challenge",
                    false,
                    "Two by Two",
                    "Breed all the animals!"
                ),
                new Logros_Minecraft
                (
                    "breed_an_animal",
                    "husbandry",
                    "task",
                    false,
                    "The Parrots and the Bats",
                    "Breed two animals together"
                ),
                new Logros_Minecraft
                (
                    "complete_catalogue",
                    "husbandry",
                    "challenge",
                    false,
                    "A Complete Catalogue",
                    "Tame all cat variants!"
                ),
                new Logros_Minecraft
                (
                    "fishy_business",
                    "husbandry",
                    "task",
                    false,
                    "Fishy Business",
                    "Catch a fish"
                ),
                new Logros_Minecraft
                (
                    "plant_seed",
                    "husbandry",
                    "task",
                    false,
                    "A Seedy Place",
                    "Plant a seed and watch it grow"
                ),
                new Logros_Minecraft
                (
                    "safely_harvest_honey",
                    "husbandry",
                    "task",
                    false,
                    "Bee Our Guest",
                    "Use a Campfire to collect Honey from a Beehive using a Bottle without aggravating the bees"
                ),
                new Logros_Minecraft
                (
                    "silk_touch_nest",
                    "husbandry",
                    "task",
                    false,
                    "Total Beelocation",
                    "Move a Bee Nest, with 3 bees inside, using Silk Touch"
                ),
                new Logros_Minecraft
                (
                    "tactical_fishing",
                    "husbandry",
                    "task",
                    false,
                    "Tactical Fishing",
                    "Catch a fish... without a fishing rod!"
                ),
                new Logros_Minecraft
                (
                    "tame_an_animal",
                    "husbandry",
                    "task",
                    false,
                    "Best Friends Forever",
                    "Tame an animal"
                ),

                // Nether advancements:
                new Logros_Minecraft
                (
                    "root",
                    "nether",
                    "task",
                    false,
                    "Nether",
                    "Bring summer clothes"
                ),
                new Logros_Minecraft
                (
                    "all_effects",
                    "nether",
                    "challenge",
                    true,
                    "How Did We Get Here?",
                    "Have every effect applied at the same time"
                ),
                new Logros_Minecraft
                (
                    "all_potions",
                    "nether",
                    "challenge",
                    false,
                    "A Furious Cocktail",
                    "Have every potion effect applied at the same time"
                ),
                new Logros_Minecraft
                (
                    "brew_potion",
                    "nether",
                    "task",
                    false,
                    "Local Brewery",
                    "Brew a potion"
                ),
                new Logros_Minecraft
                (
                    "create_beacon",
                    "nether",
                    "task",
                    false,
                    "Bring Home the Beacon",
                    "Construct and place a Beacon"
                ),
                new Logros_Minecraft
                (
                    "create_full_beacon",
                    "nether",
                    "goal",
                    false,
                    "Beaconator",
                    "Bring a beacon to full power"
                ),
                new Logros_Minecraft
                (
                    "fast_travel",
                    "nether",
                    "challenge",
                    false,
                    "Subspace Bubble",
                    "Use the Nether to travel 7 km in the Overworld"
                ),
                new Logros_Minecraft
                (
                    "find_fortress",
                    "nether",
                    "task",
                    false,
                    "A Terrible Fortress",
                    "Break your way into a Nether Fortress"
                ),
                new Logros_Minecraft
                (
                    "get_wither_skull",
                    "nether",
                    "task",
                    false,
                    "Spooky Scary Skeleton",
                    "Obtain a Wither Skeleton's skull"
                ),
                new Logros_Minecraft
                (
                    "obtain_blaze_rod",
                    "nether",
                    "task",
                    false,
                    "Into Fire",
                    "Relieve a Blaze of its rod"
                ),
                new Logros_Minecraft
                (
                    "return_to_sender",
                    "nether",
                    "challenge",
                    false,
                    "Return to Sender",
                    "Destroy a Ghast with a fireball"
                ),
                new Logros_Minecraft
                (
                    "summon_wither",
                    "nether",
                    "task",
                    false,
                    "Withering Heights",
                    "Summon the Wither"
                ),
                new Logros_Minecraft
                (
                    "uneasy_alliance",
                    "nether",
                    "challenge",
                    false,
                    "Uneasy Alliance",
                    "Rescue a Ghast from the Nether, bring it safely home to the Overworld... and then kill it"
                ),

                // Recipes advancements:
                // ...

                // Story advancements:
                new Logros_Minecraft
                (
                    "root",
                    "story",
                    "task",
                    false,
                    "Minecraft",
                    "The heart and story of the game"
                ),
                new Logros_Minecraft
                (
                    "cure_zombie_villager",
                    "story",
                    "goal",
                    false,
                    "Zombie Doctor",
                    "Weaken and then cure a Zombie Villager"
                ),
                new Logros_Minecraft
                (
                    "deflect_arrow",
                    "story",
                    "task",
                    false,
                    "Not Today, Thank You",
                    "Deflect an arrow or trident with a shield"
                ),
                new Logros_Minecraft
                (
                    "enchant_item",
                    "story",
                    "task",
                    false,
                    "Enchanter",
                    "Enchant an item at an Enchanting Table"
                ),
                new Logros_Minecraft
                (
                    "enter_the_end",
                    "story",
                    "task",
                    false,
                    "The End?",
                    "Enter the End Portal"
                ),
                new Logros_Minecraft
                (
                    "enter_the_nether",
                    "story",
                    "task",
                    false,
                    "We Need to Go Deeper",
                    "Build, light and enter a Nether Portal"
                ),
                new Logros_Minecraft
                (
                    "follow_ender_eye",
                    "story",
                    "task",
                    false,
                    "Eye Spy",
                    "Follow an Eye of Ender"
                ),
                new Logros_Minecraft
                (
                    "form_obsidian",
                    "story",
                    "task",
                    false,
                    "Ice Bucket Challenge",
                    "Form and mine a block of obsidian"
                ),
                new Logros_Minecraft
                (
                    "iron_tools",
                    "story",
                    "task",
                    false,
                    "Isn't It Iron Pick",
                    "Upgrade your pickaxe"
                ),
                new Logros_Minecraft
                (
                    "lava_bucket",
                    "story",
                    "task",
                    false,
                    "Hot Stuff",
                    "Fill a bucket with lava"
                ),
                new Logros_Minecraft
                (
                    "mine_diamond",
                    "story",
                    "task",
                    false,
                    "Diamonds!",
                    "Acquire diamonds"
                ),
                new Logros_Minecraft
                (
                    "mine_stone",
                    "story",
                    "task",
                    false,
                    "Stone Age",
                    "Mine stone with your new pickaxe"
                ),
                new Logros_Minecraft
                (
                    "obtain_armor",
                    "story",
                    "task",
                    false,
                    "Suit Up",
                    "Protect yourself with a piece of iron armor"
                ),
                new Logros_Minecraft
                (
                    "shiny_gear",
                    "story",
                    "task",
                    false,
                    "Cover Me With Diamonds",
                    "Diamond armor saves lives"
                ),
                new Logros_Minecraft
                (
                    "smelt_iron",
                    "story",
                    "task",
                    false,
                    "Acquire Hardware",
                    "Smelt an iron ingot"
                ),
                new Logros_Minecraft
                (
                    "upgrade_tools",
                    "story",
                    "task",
                    false,
                    "Getting an Upgrade",
                    "Construct a better pickaxe"
                )
            };
        }

        /// <summary>
        /// Structure that holds up all the information about a Hermit.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Logros_Jugador
        {
            internal string Ruta;
            internal string Objetivo;
            internal DateTime Fecha;
            internal bool Completado;

            internal Logros_Jugador(string Ruta, string Objetivo, DateTime Fecha, bool Completado)
            {
                this.Ruta = Ruta;
                this.Objetivo = Objetivo;
                this.Fecha = Fecha;
                this.Completado = Completado;
            }
        }

        internal readonly string Texto_Título = "Advancements Viewer by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch FPS_Cronómetro = Stopwatch.StartNew();
        internal long FPS_Segundo_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        internal bool Ocupado = false;
        /// <summary>
        /// List used to see the actual time spacing between the FPS. It can only store a full second before it resets itself.
        /// </summary>
        internal List<int> Lista_FPS_Milisegundos = new List<int>();
        /// <summary>
        /// Variable that if it's true will always show the main window on top of others.
        /// </summary>
        internal bool Variable_Siempre_Visible = false;
        internal Dictionary<string, string> Diccionario_UUID_Nombre = new Dictionary<string, string>();
        internal List<string> Lista_Rutas_Mundos = new List<string>();
        internal List<string> Lista_Rutas_Jugadores = new List<string>();
        internal List<Logros_Jugador> Lista_Logros = new List<Logros_Jugador>();

        private void Ventana_Plantilla_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                Menú_Contextual_Acerca.Text = "About " + Program.Texto_Programa + " " + Program.Texto_Versión + "...";
                this.WindowState = FormWindowState.Maximized;
                //DataGridView_Principal.Sort(Columna_Fecha, ListSortDirection.Ascending);
                this.TopMost = Variable_Siempre_Visible;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
                Cargar_Mundos();
                //UUID.Test();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_KeyDown(object sender, KeyEventArgs e)
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

        private void ComboBox_Mundo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cargar_Jugadores();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Jugador_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cargar_Logros();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Botón_Excepción_Click(object sender, EventArgs e)
        {
            try
            {
                Variable_Excepción = false;
                Variable_Excepción_Imagen = false;
                Variable_Excepción_Total = 0;
                Barra_Estado_Botón_Excepción.Visible = false;
                Barra_Estado_Separador_1.Visible = false;
                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                Barra_Estado_Botón_Excepción.Text = "Exceptions: 0";
                Ventana_Depurador_Excepciones Ventana = new Ventana_Depurador_Excepciones();
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                Menú_Contextual_Depurador_Excepciones.Text = "Exception debugger - [" + Program.Traducir_Número(Variable_Excepción_Total) + (Variable_Excepción_Total != 1 ? " exceptions" : " exception") + "]...";
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Donar_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=KSMZ3XNG2R9P6", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(this, "The help file is not available yet... sorry.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Acerca_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Acerca Ventana = new Ventana_Acerca();
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
                Barra_Estado_Botón_Excepción.Visible = false;
                Barra_Estado_Separador_1.Visible = false;
                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                Barra_Estado_Botón_Excepción.Text = "Exceptions: 0";
                Ventana_Depurador_Excepciones Ventana = new Ventana_Depurador_Excepciones();
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Abrir_Carpeta_Guardado_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Crear_Carpetas(Program.Ruta_Guardado);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Todos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Establecer_Logros();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Fondo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Establecer_Logros();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Marco_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Establecer_Logros();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Icono_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Establecer_Logros();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Temporizador_Principal_Tick(object sender, EventArgs e)
        {
            try
            {
                int Tick = Environment.TickCount; // Used in the next calculations.

                try // If there are new exceptions, flash in red text every 500 milliseconds.
                {
                    if (Variable_Excepción)
                    {
                        if ((Tick / 500) % 2 == 0)
                        {
                            if (!Variable_Excepción_Imagen)
                            {
                                Variable_Excepción_Imagen = true;
                                Barra_Estado_Botón_Excepción.Image = Resources.Excepción;
                                Barra_Estado_Botón_Excepción.ForeColor = Color.Red;
                                Barra_Estado_Botón_Excepción.Text = "Exceptions: " + Program.Traducir_Número(Variable_Excepción_Total);
                            }
                        }
                        else
                        {
                            if (Variable_Excepción_Imagen)
                            {
                                Variable_Excepción_Imagen = false;
                                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                                Barra_Estado_Botón_Excepción.Text = "Exceptions: " + Program.Traducir_Número(Variable_Excepción_Total);
                            }
                        }
                        if (!Barra_Estado_Botón_Excepción.Visible) Barra_Estado_Botón_Excepción.Visible = true;
                        if (!Barra_Estado_Separador_1.Visible) Barra_Estado_Separador_1.Visible = true;
                    }
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try // CPU and RAM use calculations.
                {
                    try
                    {
                        if (Tick % 250 == 0) // Update every 250 milliseconds.
                        {
                            if (Program.Rendimiento_Procesador != null)
                            {
                                double CPU = (double)Program.Rendimiento_Procesador.NextValue();
                                if (CPU < 0d) CPU = 0d;
                                else if (CPU > 100d) CPU = 100d;
                                Barra_Estado_Etiqueta_CPU.Text = "CPU: " + Program.Traducir_Número_Decimales_Redondear(CPU, 2) + " %";
                            }
                            Program.Proceso.Refresh();
                            long Memoria_Bytes = Program.Proceso.PagedMemorySize64;
                            Barra_Estado_Etiqueta_Memoria.Text = "RAM: " + Program.Traducir_Tamaño_Bytes_Automático(Memoria_Bytes, 2, true);
                            if (Memoria_Bytes < 4294967296L) // < 4 GB, default black text.
                            {
                                if (Variable_Memoria)
                                {
                                    Variable_Memoria = false;
                                    Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Black;
                                }
                            }
                            else // >= 4 GB, flash in red text every 500 milliseconds.
                            {
                                if ((Tick / 500) % 2 == 0)
                                {
                                    if (!Variable_Memoria)
                                    {
                                        Variable_Memoria = true;
                                        Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Red;
                                    }
                                }
                                else
                                {
                                    if (Variable_Memoria)
                                    {
                                        Variable_Memoria = false;
                                        Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Black;
                                    }
                                }
                            }
                        }
                    }
                    catch { Barra_Estado_Etiqueta_Memoria.Text = "RAM: ? MB (? GB)"; }
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try // FPS calculation and drawing.
                {
                    long FPS_Milisegundo = FPS_Cronómetro.ElapsedMilliseconds;
                    long FPS_Segundo = FPS_Milisegundo / 1000L;
                    int Milisegundo_Actual = FPS_Cronómetro.Elapsed.Milliseconds;
                    if (FPS_Segundo != FPS_Segundo_Anterior)
                    {
                        FPS_Segundo_Anterior = FPS_Segundo;
                        FPS_Real = FPS_Temporal;
                        Barra_Estado_Etiqueta_FPS.Text = FPS_Real.ToString() + " FPS";
                        FPS_Temporal = 0L;
                        Lista_FPS_Milisegundos.Clear(); // Reset.
                    }
                    Lista_FPS_Milisegundos.Add(Milisegundo_Actual); // Add the current millisecond.
                    FPS_Temporal++;

                    //if (Variable_Dibujar_Espaciado_FPS)
                    {
                        // Draw the FPS spacing in real time.
                        int Ancho_FPS = Picture_FPS.ClientSize.Width;
                        if (Ancho_FPS > 0) // Don't draw if the window is minimized.
                        {
                            Bitmap Imagen_FPS = new Bitmap(Ancho_FPS, 8, PixelFormat.Format32bppArgb);
                            Graphics Pintar_FPS = Graphics.FromImage(Imagen_FPS);
                            Pintar_FPS.CompositingMode = CompositingMode.SourceOver;
                            Pintar_FPS.CompositingQuality = CompositingQuality.HighQuality;
                            Pintar_FPS.InterpolationMode = InterpolationMode.NearestNeighbor;
                            Pintar_FPS.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Pintar_FPS.SmoothingMode = SmoothingMode.None;
                            Pintar_FPS.TextRenderingHint = TextRenderingHint.AntiAlias;
                            Ancho_FPS -= 8; // Subtract 8 pixels to draw the full FPS icons on the image borders.
                            foreach (int Milisegundo in Lista_FPS_Milisegundos)
                            {
                                SolidBrush Pincel = new SolidBrush(Program.Obtener_Color_Puro_1530((Milisegundo * 1529) / 999));
                                Pintar_FPS.FillEllipse(Pincel, ((Milisegundo * Ancho_FPS) / 999), 0, 8, 8);
                                Pincel.Dispose();
                                Pincel = null;
                            }
                            Pintar_FPS.Dispose();
                            Pintar_FPS = null;
                            Picture_FPS.BackgroundImage = Imagen_FPS;
                        }
                    }
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Cargar_Mundos()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.Text = Texto_Título + " - [Loading the existing worlds...]";
                ComboBox_Mundo.Items.Clear();
                ComboBox_Jugador.Items.Clear();
                DataGridView_Principal.Rows.Clear();
                Lista_Rutas_Mundos.Clear();
                Lista_Rutas_Jugadores.Clear();
                Lista_Logros.Clear();
                // Load the existing Minecraft worlds from the default save folder and of any existing modpacks:
                if (Directory.Exists(Program.Ruta_Guardado_Minecraft))
                {
                    string[] Matriz_Rutas = Directory.GetDirectories(Program.Ruta_Guardado_Minecraft, "*", SearchOption.TopDirectoryOnly);
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        if (Matriz_Rutas.Length > 1) Array.Sort(Matriz_Rutas);
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            Lista_Rutas_Mundos.Add(Ruta);
                            ComboBox_Mundo.Items.Add(Path.GetFileName(Ruta));
                        }
                        Matriz_Rutas = null;
                    }
                }
                this.Text = Texto_Título + " - [Found worlds: " + Program.Traducir_Número(ComboBox_Mundo.Items.Count) + ", Found players: " + Program.Traducir_Número(ComboBox_Jugador.Items.Count) + ", Found advancements: " + Program.Traducir_Número(DataGridView_Principal.Rows.Count) + " of " + Program.Traducir_Número(Logros_Minecraft.Matriz_Logros_Minecraft.Length) + "]";
                if (ComboBox_Mundo.Items.Count > 0) ComboBox_Mundo.SelectedIndex = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal void Cargar_Jugadores()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.Text = Texto_Título + " - [Loading the existing players...]";
                ComboBox_Jugador.Items.Clear();
                DataGridView_Principal.Rows.Clear();
                Lista_Rutas_Jugadores.Clear();
                Lista_Logros.Clear();
                int Índice_Mundo = ComboBox_Mundo.SelectedIndex;
                if (Índice_Mundo > -1 && Índice_Mundo < Lista_Rutas_Mundos.Count)
                {
                    string Ruta_Mundo = Lista_Rutas_Mundos[Índice_Mundo] + "\\advancements";
                    if (Directory.Exists(Ruta_Mundo))
                    {
                        string[] Matriz_Rutas = Directory.GetFiles(Ruta_Mundo, "*.json", SearchOption.TopDirectoryOnly);
                        if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                        {
                            if (Matriz_Rutas.Length > 1) Array.Sort(Matriz_Rutas);
                            foreach (string Ruta in Matriz_Rutas)
                            {
                                Lista_Rutas_Jugadores.Add(Ruta);
                                string UUID = Path.GetFileNameWithoutExtension(Ruta);
                                ComboBox_Jugador.Items.Add(UUID + (!Program.Diccionario_UUIDs_Nombres.ContainsKey(UUID) ? null : " (" + Program.Diccionario_UUIDs_Nombres[UUID] + ")"));
                            }
                            Matriz_Rutas = null;
                        }
                    }
                }
                this.Text = Texto_Título + " - [Found worlds: " + Program.Traducir_Número(ComboBox_Mundo.Items.Count) + ", Found players: " + Program.Traducir_Número(ComboBox_Jugador.Items.Count) + ", Found advancements: " + Program.Traducir_Número(DataGridView_Principal.Rows.Count) + " of " + Program.Traducir_Número(Logros_Minecraft.Matriz_Logros_Minecraft.Length) + "]";
                if (ComboBox_Jugador.Items.Count > 0) ComboBox_Jugador.SelectedIndex = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal void Cargar_Logros()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string Ruta_Jugador = null;
                this.Text = Texto_Título + " - [Loading the selected player...]";
                DataGridView_Principal.Rows.Clear();
                Lista_Logros.Clear();
                int Índice_Jugador = ComboBox_Jugador.SelectedIndex;
                if (ComboBox_Mundo.SelectedIndex > -1 &&
                    ComboBox_Mundo.SelectedIndex < Lista_Rutas_Mundos.Count &&
                    Índice_Jugador > -1 &&
                    Índice_Jugador < Lista_Rutas_Jugadores.Count)
                {
                    Ruta_Jugador = Lista_Rutas_Jugadores[Índice_Jugador];
                    if (File.Exists(Ruta_Jugador))
                    {
                        FileStream Lector = new FileStream(Ruta_Jugador, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        if (Lector != null && Lector.Length > 0L)
                        {
                            Lector.Seek(0L, SeekOrigin.Begin);
                            StreamReader Lector_Texto = new StreamReader(Lector, Encoding.UTF8, true);
                            while (!Lector_Texto.EndOfStream)
                            {
                                try
                                {
                                    string Línea = Lector_Texto.ReadLine();
                                    if (!string.IsNullOrEmpty(Línea))
                                    {
                                        string Ruta = null;
                                        string Objetivo = null;
                                        DateTime Fecha = DateTime.MinValue;
                                        bool Completado = false;
                                        if (Línea.Contains("minecraft:") && !Línea.Contains("minecraft:recipes")) // Assume advancements.
                                        {
                                            // Try to load the path.
                                            try
                                            {
                                                string Texto_Ruta = "\"";
                                                int Índice_Ruta_Inicio = Línea.IndexOf(Texto_Ruta, StringComparison.InvariantCultureIgnoreCase);
                                                int Índice_Ruta_Fin = Línea.LastIndexOf(Texto_Ruta, StringComparison.InvariantCultureIgnoreCase);
                                                if (Índice_Ruta_Inicio > -1 && Índice_Ruta_Fin > -1 && Índice_Ruta_Inicio < Índice_Ruta_Fin)
                                                {
                                                    Índice_Ruta_Inicio += Texto_Ruta.Length;
                                                    Ruta = Línea.Substring(Índice_Ruta_Inicio, Índice_Ruta_Fin - Índice_Ruta_Inicio);
                                                }
                                            }
                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Ruta = null; }

                                            Lector_Texto.ReadLine(); // Assume "    \"criteria\": {".

                                            string Línea_Objetivo_Fecha = Lector_Texto.ReadLine();
                                            if (!string.IsNullOrEmpty(Línea_Objetivo_Fecha))
                                            {
                                                // Try to load the objective.
                                                try
                                                {
                                                    string Texto_Objetivo = "\"";
                                                    int Índice_Objetivo_Inicio = Línea_Objetivo_Fecha.IndexOf(Texto_Objetivo, StringComparison.InvariantCultureIgnoreCase);
                                                    if (Índice_Objetivo_Inicio > -1)
                                                    {
                                                        Índice_Objetivo_Inicio += Texto_Objetivo.Length;
                                                        int Índice_Objetivo_Fin = Línea_Objetivo_Fecha.IndexOf("\"", Índice_Objetivo_Inicio, StringComparison.InvariantCultureIgnoreCase);
                                                        if (Índice_Objetivo_Fin > -1)
                                                        {
                                                            Objetivo = Línea_Objetivo_Fecha.Substring(Índice_Objetivo_Inicio, Índice_Objetivo_Fin - Índice_Objetivo_Inicio);
                                                        }
                                                    }
                                                }
                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Objetivo = null; }

                                                // Try to load the first date.
                                                try
                                                {
                                                    string Texto_Fecha = "\": \"";
                                                    int Índice_Fecha_Inicio = Línea_Objetivo_Fecha.IndexOf(Texto_Fecha, StringComparison.InvariantCultureIgnoreCase);
                                                    if (Índice_Fecha_Inicio > -1)
                                                    {
                                                        Índice_Fecha_Inicio += Texto_Fecha.Length;
                                                        int Año = int.Parse(Línea_Objetivo_Fecha.Substring(Índice_Fecha_Inicio, 4));
                                                        int Mes = int.Parse(Línea_Objetivo_Fecha.Substring(Índice_Fecha_Inicio + 5, 2));
                                                        int Día = int.Parse(Línea_Objetivo_Fecha.Substring(Índice_Fecha_Inicio + 8, 2));
                                                        int Hora = int.Parse(Línea_Objetivo_Fecha.Substring(Índice_Fecha_Inicio + 11, 2));
                                                        int Minuto = int.Parse(Línea_Objetivo_Fecha.Substring(Índice_Fecha_Inicio + 14, 2));
                                                        int Segundo = int.Parse(Línea_Objetivo_Fecha.Substring(Índice_Fecha_Inicio + 17, 2));
                                                        Fecha = new DateTime(Año, Mes, Día, Hora, Minuto, Segundo);
                                                    }
                                                }
                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Fecha = DateTime.MinValue; }
                                            }
                                            Línea_Objetivo_Fecha = null;

                                            //Lector_Texto.ReadLine(); // Assume "    },". // This wasn't working all times.
                                            string Texto_Completado = "\"done\": ";
                                            // Try to skip several possible lines and reach the status line.
                                            while (!Lector_Texto.EndOfStream)
                                            {
                                                try
                                                {
                                                    string Línea_Completado = Lector_Texto.ReadLine();
                                                    if (!string.IsNullOrEmpty(Línea_Completado))
                                                    {
                                                        // Try to load the status.
                                                        try
                                                        {
                                                            int Índice_Completado_Inicio = Línea_Completado.IndexOf(Texto_Completado, StringComparison.InvariantCultureIgnoreCase);
                                                            if (Índice_Completado_Inicio > -1)
                                                            {
                                                                Índice_Completado_Inicio += Texto_Completado.Length;
                                                                Completado = string.Compare(Línea_Completado.Substring(Índice_Completado_Inicio), "true", true) == 0;
                                                                break;
                                                            }
                                                        }
                                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Completado = false; }
                                                    }
                                                    Línea_Completado = null;
                                                }
                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                            }

                                            Lector_Texto.ReadLine(); // Assume "  },".

                                            Lista_Logros.Add(new Logros_Jugador(Ruta, Objetivo, Fecha, Completado));
                                        }
                                        /*else if (Línea.Contains("DataVersion:")) // Assume end of file.
                                        {
                                            break;
                                        }*/
                                        Línea = null;
                                    }
                                }
                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                            }
                            Lector_Texto.Close();
                            Lector_Texto.Dispose();
                            Lector_Texto = null;
                            Lector.Close();
                            Lector.Dispose();
                            Lector = null;
                        }
                    }
                }
                this.Text = Texto_Título + " - [Found worlds: " + Program.Traducir_Número(ComboBox_Mundo.Items.Count) + ", Found players: " + Program.Traducir_Número(ComboBox_Jugador.Items.Count) + ", Found advancements: " + Program.Traducir_Número(DataGridView_Principal.Rows.Count) + " of " + Program.Traducir_Número(Logros_Minecraft.Matriz_Logros_Minecraft.Length) + "]";
                Establecer_Logros();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal Bitmap Obtener_Imagen_Logro(string Recurso_Logro, string Recurso_Marco, string Recurso_Fondo)
        {
            try
            {
                int Ancho_Alto_Máximo = 1; // This should compact vertically the list of advancements.
                if (Menú_Contextual_Dibujar_Fondo.Checked) Ancho_Alto_Máximo = 64;
                else if (Menú_Contextual_Dibujar_Marco.Checked) Ancho_Alto_Máximo = 52;
                else if (Menú_Contextual_Dibujar_Icono.Checked) Ancho_Alto_Máximo = 32;

                Bitmap Imagen_Fondo = Menú_Contextual_Dibujar_Fondo.Checked ? Program.Obtener_Imagen_Recursos(Recurso_Fondo) : null;
                Bitmap Imagen_Marco = Menú_Contextual_Dibujar_Marco.Checked ? Program.Obtener_Imagen_Recursos(Recurso_Marco) : null;
                Bitmap Imagen_Logro = Menú_Contextual_Dibujar_Icono.Checked ? Program.Obtener_Imagen_Recursos(Recurso_Logro) : null;

                if (Imagen_Fondo == null) Imagen_Fondo = new Bitmap(Ancho_Alto_Máximo, Ancho_Alto_Máximo, PixelFormat.Format32bppArgb);
                if (Imagen_Marco == null) Imagen_Marco = new Bitmap(Ancho_Alto_Máximo, Ancho_Alto_Máximo, PixelFormat.Format32bppArgb);
                if (Imagen_Logro == null) Imagen_Logro = new Bitmap(Ancho_Alto_Máximo, Ancho_Alto_Máximo, PixelFormat.Format32bppArgb);

                int Ancho_Logro = Imagen_Logro.Width;
                int Alto_Logro = Imagen_Logro.Height;
                int Ancho_Fondo = Imagen_Fondo.Width;
                int Alto_Fondo = Imagen_Fondo.Height;
                int Ancho_Marco = Imagen_Marco.Width;
                int Alto_Marco = Imagen_Marco.Height;

                Graphics Pintar = Graphics.FromImage(Imagen_Fondo);
                Pintar.CompositingMode = CompositingMode.SourceOver;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.None;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;

                Pintar.DrawImage(Imagen_Marco, new Rectangle((Ancho_Fondo - Ancho_Marco) / 2, (Alto_Fondo - Alto_Marco) / 2, Ancho_Marco, Alto_Marco), new Rectangle(0, 0, Ancho_Marco, Alto_Marco), GraphicsUnit.Pixel);
                Pintar.DrawImage(Imagen_Logro, new Rectangle((Ancho_Fondo - Ancho_Logro) / 2, (Alto_Fondo - Alto_Logro) / 2, Ancho_Logro, Alto_Logro), new Rectangle(0, 0, Ancho_Logro, Alto_Logro), GraphicsUnit.Pixel);
                Pintar.Dispose();
                Pintar = null;
                return Imagen_Fondo;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal void Establecer_Logros()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!Ocupado)
                {
                    Ocupado = true;
                    DataGridView_Principal.Rows.Clear();
                    if (Minecraft.Bloques.Matriz_Bloques != null && Minecraft.Bloques.Matriz_Bloques.Length > 0)
                    {
                        DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        if (Logros_Minecraft.Matriz_Logros_Minecraft != null && Logros_Minecraft.Matriz_Logros_Minecraft.Length > 0)
                        {
                            if (!Menú_Contextual_Mostrar_Todos.Checked) // Show only all the found advancements.
                            {
                                if (Lista_Logros != null && Lista_Logros.Count > 0)
                                {
                                    foreach (Logros_Jugador Logro_Jugador in Lista_Logros)
                                    {
                                        try
                                        {
                                            bool Encontrado = false;
                                            foreach (Logros_Minecraft Logro_Minecraft in Logros_Minecraft.Matriz_Logros_Minecraft)
                                            {
                                                try
                                                {
                                                    if (Logro_Jugador.Ruta.Contains(Logro_Minecraft.Categoría + "/" + Logro_Minecraft.Ruta_Recurso))
                                                    {
                                                        DataGridView_Principal.Rows.Add(new object[]
                                                        {
                                                            Obtener_Imagen_Logro("Logros_" + Logro_Minecraft.Categoría + "_" + Logro_Minecraft.Ruta_Recurso, "Logros_Marco_" + Logro_Minecraft.Marco, "Logros_Fondo_" + Logro_Minecraft.Categoría),
                                                            Logro_Minecraft.Título,
                                                            Logro_Minecraft.Categoría.Substring(0, 1).ToUpperInvariant() + Logro_Minecraft.Categoría.Substring(1),
                                                            Logro_Minecraft.Marco.Substring(0, 1).ToUpperInvariant() + Logro_Minecraft.Marco.Substring(1),
                                                            Logro_Minecraft.Oculto,
                                                            Logro_Jugador.Fecha,
                                                            Logro_Jugador.Completado,
                                                            Logro_Minecraft.Descripción,
                                                            "minecraft:" + Logro_Minecraft.Categoría + "/" + Logro_Minecraft.Ruta_Recurso
                                                        });
                                                        Encontrado = true;
                                                        break;
                                                    }
                                                }
                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                            }
                                            if (!Encontrado) // Possibly a new advancement?
                                            {
                                                DataGridView_Principal.Rows.Add(new object[]
                                                {
                                                    Obtener_Imagen_Logro("Logros_story_root", "Logros_Marco_task", "Logros_Fondo_story"),
                                                    "?",
                                                    "?",
                                                    "?",
                                                    false,
                                                    Logro_Jugador.Fecha,
                                                    Logro_Jugador.Completado,
                                                    "?",
                                                    Logro_Jugador.Ruta
                                                });
                                            }
                                        }
                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                    }
                                }
                            }
                            else // This will show all the known advancements, but it might also exclude new unknown ones.
                            {
                                foreach (Logros_Minecraft Logro_Minecraft in Logros_Minecraft.Matriz_Logros_Minecraft)
                                {
                                    try
                                    {
                                        bool Encontrado = false;
                                        foreach (Logros_Jugador Logro_Jugador in Lista_Logros)
                                        {
                                            try
                                            {
                                                if (Logro_Jugador.Ruta.Contains(Logro_Minecraft.Categoría + "/" + Logro_Minecraft.Ruta_Recurso))
                                                {
                                                    DataGridView_Principal.Rows.Add(new object[]
                                                    {
                                                        Obtener_Imagen_Logro("Logros_" + Logro_Minecraft.Categoría + "_" + Logro_Minecraft.Ruta_Recurso, "Logros_Marco_" + Logro_Minecraft.Marco, "Logros_Fondo_" + Logro_Minecraft.Categoría),
                                                        Logro_Minecraft.Título,
                                                        Logro_Minecraft.Categoría.Substring(0, 1).ToUpperInvariant() + Logro_Minecraft.Categoría.Substring(1),
                                                        Logro_Minecraft.Marco.Substring(0, 1).ToUpperInvariant() + Logro_Minecraft.Marco.Substring(1),
                                                        Logro_Minecraft.Oculto,
                                                        Logro_Jugador.Fecha,
                                                        Logro_Jugador.Completado,
                                                        Logro_Minecraft.Descripción,
                                                        "minecraft:" + Logro_Minecraft.Categoría + "/" + Logro_Minecraft.Ruta_Recurso
                                                    });
                                                    Encontrado = true;
                                                    break;
                                                }
                                            }
                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                        }
                                        if (!Encontrado) // An advancement still not unlocked by the current player.
                                        {
                                            DataGridView_Principal.Rows.Add(new object[]
                                            {
                                                Obtener_Imagen_Logro("Logros_" + Logro_Minecraft.Categoría + "_" + Logro_Minecraft.Ruta_Recurso, "Logros_Marco_" + Logro_Minecraft.Marco, "Logros_Fondo_" + Logro_Minecraft.Categoría),
                                                Logro_Minecraft.Título,
                                                Logro_Minecraft.Categoría.Substring(0, 1).ToUpperInvariant() + Logro_Minecraft.Categoría.Substring(1),
                                                Logro_Minecraft.Marco.Substring(0, 1).ToUpperInvariant() + Logro_Minecraft.Marco.Substring(1),
                                                Logro_Minecraft.Oculto,
                                                DateTime.MinValue,
                                                false,
                                                Logro_Minecraft.Descripción,
                                                "minecraft:" + Logro_Minecraft.Categoría + "/" + Logro_Minecraft.Ruta_Recurso
                                            });
                                        }
                                    }
                                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                }
                            }
                        }
                        DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                        this.Text = Texto_Título + " - [Found worlds: " + Program.Traducir_Número(ComboBox_Mundo.Items.Count) + ", Found players: " + Program.Traducir_Número(ComboBox_Jugador.Items.Count) + ", Found advancements: " + Program.Traducir_Número(DataGridView_Principal.Rows.Count) + " of " + Program.Traducir_Número(Logros_Minecraft.Matriz_Logros_Minecraft.Length) + "]";
                        if (DataGridView_Principal.Rows.Count > 0)
                        {
                            if (DataGridView_Principal.SortedColumn != null)
                            {
                                DataGridView_Principal.Sort(DataGridView_Principal.SortedColumn, DataGridView_Principal.SortOrder != SortOrder.Descending ? ListSortDirection.Ascending : ListSortDirection.Descending);
                            }
                            DataGridView_Principal.CurrentCell = DataGridView_Principal[Columna_Título.Index, 0];
                        }
                    }
                    Ocupado = false;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
