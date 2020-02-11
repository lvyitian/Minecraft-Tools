using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    internal static class Xbox_360
    {
        internal static Dictionary<string, string> Diccionario_Nombres_Packs_Recursos = new Dictionary<string, string>();

        internal static /*List<string>*/SortedDictionary<long, string> Obtener_Nombres_Packs_Recursos(string Ruta, string Ruta_Salida, bool Modo_Skins)
        {
            SortedDictionary<long, string> Diccionario_Índices_Rutas = new SortedDictionary<long, string>();
            //string Ruta = @"C: \Users\Jupisoft\Pictures\XBOX\res\DLC\Adventure Time\Data\x16Data.pck";
            //string Ruta = @"C:\Users\Jupisoft\Pictures\XBOX\res\DLC\Natural\Data\x32Data.pck";
            if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
            {
                FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                Lector.Seek(0L, SeekOrigin.Begin);
                StreamReader Lector_Texto = new StreamReader(Lector, Encoding.Unicode);
                List<char> Lista_Caracteres = new List<char>();
                byte[] Matriz_Bytes = new byte[4096];
                for (long Índice_Bloque = 0L; Índice_Bloque < Lector.Length; Índice_Bloque += Matriz_Bytes.LongLength)
                {
                    int Longitud = Lector.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    for (long Índice = 0L; Índice < Longitud; Índice++)
                    {
                        char Caracter = (char)Matriz_Bytes[Índice];
                        if (!char.IsControl(Caracter)) Lista_Caracteres.Add(Caracter);
                        else Lista_Caracteres.Add('?'); // Keep the original file length.
                    }
                }
                /*foreach (char Caracter in Lector_Texto.ReadToEnd())
                {
                    if (!char.IsControl(Caracter)) Texto += Caracter.ToString();
                }*/
                /*for (long Índice = 0L; Índice < Lector.Length; Índice++)
                {
                    char Caracter = (char)Lector_Texto.ReadByte();
                    if (!char.IsControl(Caracter)) Texto += Caracter.ToString();
                }*/
                if (Lista_Caracteres.Count > 0)
                {
                    string Texto = new string(Lista_Caracteres.ToArray());
                    //File.WriteAllText(Program.Obtener_Ruta_Temporal_Escritorio() + ".txt", Texto);
                    //Clipboard.SetText(Texto);
                    int Total = 0;
                    int Índice_Anterior = 0;
                    if (!Modo_Skins)
                    {
                        while (true)
                        {
                            int Índice_Barra = Texto.IndexOf("res/", Índice_Anterior);
                            if (Índice_Barra > -1)
                            {
                                int Índice_PNG = Texto.IndexOf(".png", Índice_Barra);
                                int Índice_TGA = Texto.IndexOf(".tga", Índice_Barra);
                                if (Índice_PNG > -1 && Índice_TGA > -1)
                                {
                                    if (Índice_PNG < 0) Índice_PNG = Índice_TGA; // Avoid negative comparisons.
                                    if (Índice_TGA < 0) Índice_TGA = Índice_PNG;
                                    int Índice_PNG_TGA = Math.Min(Índice_PNG, Índice_TGA); // Get the nearest index.
                                    if (Índice_PNG_TGA > -1)
                                    {
                                        Diccionario_Índices_Rutas.Add(Índice_PNG_TGA + 4, Texto.Substring(Índice_Barra, ((Índice_PNG_TGA + 4) - Índice_Barra)));
                                        //Líneas += Texto.Substring(Índice_Barra + 0, ((Índice_PNG + 4) - Índice_Barra)) + "\r\n";
                                        //Líneas += "Diccionario_Nombres_Packs_Recursos.Add(\"" + Texto.Substring(Índice_Barra + 0, ((Índice_PNG + 4) - Índice_Barra)) + "\", \"0000000001\");\r\n";
                                        Total++;
                                        Índice_Anterior = Índice_PNG_TGA;
                                    }
                                    else break;
                                }
                                else break;
                            }
                            else break;
                        }
                    }
                    else
                    {
                        List<int> Lista_Índices = new List<int>();
                        while (true)
                        {
                            int Índice = Texto.IndexOf("DISPLAYNAME", Índice_Anterior);
                            if (Índice > -1)
                            {
                                Lista_Índices.Add(Índice);
                                Total++;
                                Índice_Anterior = Índice + 1;
                            }
                            else break;
                        }
                        //MessageBox.Show(Lista_Índices.Count.ToString(), Total.ToString());
                        /*while (true)
                        {
                            //int Índice_Inicio = Texto.IndexOf("\0\0\0" + (char)17 + "\0\0\0\0\0\0\0", Índice_Anterior);
                            //int Índice_Inicio = Texto.IndexOf("IEND®B`", Índice_Anterior);
                            int Índice_Inicio = Texto.IndexOf("IEND", Índice_Anterior);
                            if (Índice_Inicio > -1)
                            {
                                int Índice_Fin = Texto.IndexOf("IDS_DLCSKIN", Índice_Inicio);
                                if (Índice_Fin > -1)
                                {
                                    string Nombre_Temporal = Texto.Substring(Índice_Inicio + 7, ((Índice_Fin + 0) - (Índice_Inicio + 7)));
                                    string Nombre = null;
                                    foreach (char Caracter in Nombre_Temporal)
                                    {
                                        if (!char.IsControl(Caracter)) Nombre += Caracter;
                                    }
                                    //Program.Traducir_Nombre_Archivo(Nombre);
                                    Lista_Rutas.Add(Nombre);
                                    //Líneas += Texto.Substring(Índice_Barra + 0, ((Índice_PNG + 4) - Índice_Barra)) + "\r\n";
                                    //Líneas += "Diccionario_Nombres_Packs_Recursos.Add(\"" + Texto.Substring(Índice_Barra + 0, ((Índice_PNG + 4) - Índice_Barra)) + "\", \"0000000001\");\r\n";
                                    Total++;
                                    Índice_Anterior = Índice_Fin;
                                }
                                else break;
                            }
                            else break;
                        }*/

                        /**//*if (Lista_Índices.Count % 2 != 0) MessageBox.Show(Lista_Índices.Count.ToString(), "% 2 != 0");
                        Lista_Índices.RemoveRange(0, Lista_Índices.Count / 4);
                        for (int Índice = 0; Índice < Lista_Índices.Count; Índice++)
                        {
                            Lista_Rutas.Add(Texto.Substring((Lista_Índices[Índice] - 20) - 64, 64));
                        }*//**/

                        /*int Índice_Total = 0;
                        for (int Índice = 0; Índice < Lista_Rutas.Count + 1; Índice++)
                        {
                            Índice_Total = Texto.IndexOf("_DISPLAYNAME", Índice_Total + 1);
                        }
                        Índice_Total = Texto.IndexOf("_DISPLAYNAME", Índice_Total + 1);
                        Lista_Rutas.Insert(0, Texto.Substring(Índice_Total - 51, 32));*/
                    }
                    //Lista_Rutas.Sort();
                    FileStream Lector_Salida = new FileStream(Ruta_Salida, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
                    StreamWriter Lector_Salida_Texto = new StreamWriter(Lector_Salida, Encoding.Unicode);
                    string Líneas = null;
                    foreach (KeyValuePair<long, string> Entrada in Diccionario_Índices_Rutas)
                    {
                        //if (!Diccionario_Nombres_Packs_Recursos.ContainsKey(Línea)) Líneas += "Diccionario_Nombres_Packs_Recursos.Add(\"" + Línea + "\", \"0000000001\");\r\n";
                        //Líneas += "Diccionario_Nombres_Packs_Recursos.Add(\"" + Línea + "\", \"0000000001\");\r\n";
                        Lector_Salida_Texto.WriteLine("Diccionario_Nombres_Packs_Recursos.Add(" + Entrada.Key.ToString() + "L, \"" + Entrada.Value + "\", \"0000000001\");");
                        Lector_Salida_Texto.Flush();
                    }
                    Lector_Salida_Texto.Close();
                    Lector_Salida_Texto.Dispose();
                    Lector_Salida_Texto = null;
                    Lector_Salida.Close();
                    Lector_Salida.Dispose();
                    Lector_Salida = null;
                    //SystemSounds.Asterisk.Play();
                    //MessageBox.Show(Total.ToString());
                    /*if (!string.IsNullOrEmpty(Líneas))
                    {
                        //Clipboard.SetText(Líneas);
                        //MessageBox.Show(Ruta);
                        SystemSounds.Asterisk.Play();
                    }*/
                }
                Lector_Texto.Close();
                Lector_Texto.Dispose();
                Lector_Texto = null;
                Lector.Close();
                Lector.Dispose();
                Lector = null;
            }
            return Diccionario_Índices_Rutas.Count > 0 ? Diccionario_Índices_Rutas : null;
        }

        internal static bool Verificar_Imagen_32_Bits_Transparente(Bitmap Imagen)
        {
            if (Imagen != null && Image.IsAlphaPixelFormat(Imagen.PixelFormat))
            {
                int Ancho = Imagen.Width;
                int Alto = Imagen.Height;
                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                Imagen.UnlockBits(Bitmap_Data);
                for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                {
                    for (int X = 0; X < Ancho; X++, Índice += 4)
                    {
                        if (Matriz_Bytes[Índice + 3] > 0)
                        {
                            Matriz_Bytes = null;
                            return false;
                        }
                    }
                }
                Matriz_Bytes = null;
                return true;
            }
            return false;
        }

        internal static void Crear_Packs_Skins_Renombrar()
        {
            string Ruta_Base = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Skins";
            if (!string.IsNullOrEmpty(Ruta_Base) && Directory.Exists(Ruta_Base))
            {
                string[] Matriz_Carpetas = Directory.GetDirectories(Ruta_Base, "*", SearchOption.TopDirectoryOnly);
                if (Matriz_Carpetas != null && Matriz_Carpetas.Length > 0)
                {
                    foreach (string Ruta_Entrada in Matriz_Carpetas)
                    {
                        string Pack_Recursos = Path.GetFileNameWithoutExtension(Ruta_Entrada);
                        string[] Matriz_Nombres = File.ReadAllLines(Ruta_Base + "\\" + Pack_Recursos + ".txt", Encoding.Unicode);
                        for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                        {
                            if (!string.IsNullOrEmpty(Matriz_Nombres[Índice])) Matriz_Nombres[Índice] = Matriz_Nombres[Índice].Trim().Replace("\"", "'").Replace("\\", "-").Replace("/", "-").Replace(":", "-");
                        }
                        string[] Matriz_Rutas = Directory.GetFiles(Ruta_Entrada, "*.png", SearchOption.TopDirectoryOnly);
                        if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                        {
                            for (int Índice_Ruta = 0; Índice_Ruta < Matriz_Rutas.Length; Índice_Ruta++)
                            {
                                string Número = (Índice_Ruta + 1).ToString();
                                while (Número.Length < 2) Número = '0' + Número;
                                string Ruta_PNG = Path.GetDirectoryName(Matriz_Rutas[Índice_Ruta]) + "\\" + Número + (Índice_Ruta < Matriz_Nombres.Length && !string.IsNullOrEmpty(Matriz_Nombres[Índice_Ruta]) ? " " + Matriz_Nombres[Índice_Ruta] : null);
                                while (File.Exists(Ruta_PNG + ".png")) Ruta_PNG += '_';
                                File.Move(Matriz_Rutas[Índice_Ruta], Ruta_PNG + ".png");
                            }
                        }
                    }
                }
            }
        }

        internal static void Crear_Packs_Skins()
        {
            string Ruta_Base = @"C:\Program Files (x86)\Image-Line\FL Studio 9\Data\Projects\__JUPISOFT COMPILATION VOL. 1\Skins";
            if (!string.IsNullOrEmpty(Ruta_Base) && Directory.Exists(Ruta_Base))
            {
                string[] Matriz_Carpetas = Directory.GetDirectories(Ruta_Base, "*", SearchOption.TopDirectoryOnly);
                if (Matriz_Carpetas != null && Matriz_Carpetas.Length > 0)
                {
                    foreach (string Ruta_Entrada in Matriz_Carpetas)
                    {
                        string Pack_Recursos = Path.GetFileNameWithoutExtension(Ruta_Entrada);
                        List<string> Lista_Recursos = new List<string>(); //Obtener_Nombres_Packs_Recursos(Ruta_Entrada + "\\a.pck", true);
                        string[] Matriz_Rutas = Directory.GetFiles(Ruta_Entrada, "*.png", SearchOption.TopDirectoryOnly);
                        if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                        {
                            Array.Sort(Matriz_Rutas);
                            if (Lista_Recursos.Count != Matriz_Rutas.Length) MessageBox.Show(Lista_Recursos.Count.ToString(), Matriz_Rutas.Length.ToString());
                            string Ruta_Salida = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Skins\\" + Pack_Recursos;
                            string Texto_Nombres = null;
                            foreach (string Nombre in Lista_Recursos) Texto_Nombres += Nombre + "\r\n";
                            if (!File.Exists(Ruta_Base + "\\" + Pack_Recursos + ".txt")) File.WriteAllText(Ruta_Base + "\\" + Pack_Recursos + ".txt", Texto_Nombres, UnicodeEncoding.Unicode);
                            //continue;
                            for (int Índice_Ruta = 0; Índice_Ruta < Matriz_Rutas.Length; Índice_Ruta++)
                            {
                                FileStream Lector = new FileStream(Matriz_Rutas[Índice_Ruta], FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                                Image Imagen_Original = null;
                                try { Imagen_Original = Image.FromStream(Lector, false, false); }
                                catch { Imagen_Original = null; }
                                if (Imagen_Original != null)
                                {
                                    int Ancho_Original = Imagen_Original.Width;
                                    int Alto_Original = Imagen_Original.Height;
                                    string Nombre = Índice_Ruta < Lista_Recursos.Count ? Lista_Recursos[Índice_Ruta] : Índice_Ruta.ToString();
                                    if (Nombre.Length > 64) Nombre = Nombre.Substring(0, 64);
                                    int Ancho = Ancho_Original;
                                    int Alto = Alto_Original;
                                    /*List<string> Lista_Ancho_Alto = new List<string>(new string[]
                                    {
                                        "res/mob/pigzombie.png",
                                        "res/mob/zombie.png",
                                        "res/mob/zombie/husk.png"
                                    }); // Reparar texturas
                                    if (Lista_Ancho_Alto.Contains(Lista_Recursos[Índice_Ruta]))
                                    {
                                        if (Ancho != Alto)
                                        {
                                            Ancho = Math.Max(Ancho, Alto);
                                            Alto = Ancho;
                                        }
                                    }*/
                                    Bitmap Imagen = new Bitmap(Ancho, Alto, !Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format24bppRgb : PixelFormat.Format32bppArgb);
                                    Graphics Pintar = Graphics.FromImage(Imagen);
                                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho_Original, Alto_Original), new Rectangle(0, 0, Ancho_Original, Alto_Original), GraphicsUnit.Pixel);
                                    Pintar.Dispose();
                                    Pintar = null;
                                    Imagen_Original.Dispose();
                                    Imagen_Original = null;
                                    //string Ruta_PNG = Ruta_Salida + "\\" + Nombre.Replace("\"", "'") + ".png";
                                    string Ruta_PNG = Ruta_Salida + "\\" + Path.GetFileNameWithoutExtension(Matriz_Rutas[Índice_Ruta]).Replace("\"", "'") + ".png";
                                    Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_PNG));
                                    try { Imagen.Save(Ruta_PNG, ImageFormat.Png); }
                                    catch { MessageBox.Show(Nombre); }
                                    Imagen.Dispose();
                                    Imagen = null;
                                    Ruta_PNG = null;
                                }
                            }
                        }
                    }
                }
            }
        }

        internal static void Reparar_Índice_Imágenes(string Ruta_Entrada, int Número_Entrada)
        {
            if (!string.IsNullOrEmpty(Ruta_Entrada) && Directory.Exists(Ruta_Entrada))
            {
                string[] Matriz_Rutas = Directory.GetFiles(Ruta_Entrada, "*.png", SearchOption.TopDirectoryOnly);
                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                {
                    Array.Sort(Matriz_Rutas);
                    Array.Reverse(Matriz_Rutas);
                    foreach (string Ruta in Matriz_Rutas)
                    {
                        int Número = int.Parse(Path.GetFileNameWithoutExtension(Ruta));
                        if (Número > Número_Entrada)
                        {
                            Número++;
                            string Nombre = Número.ToString();
                            while (Nombre.Length < 10) Nombre = '0' + Nombre;
                            if (!File.Exists(Ruta_Entrada + "\\" + Nombre + ".png")) File.Move(Ruta, Ruta_Entrada + "\\" + Nombre + ".png");
                            else MessageBox.Show(Ruta_Entrada + "\\" + Nombre + ".png", "¿Ya existe?");
                        }
                    }
                }
            }
        }

        internal static void Crear_Packs_Recursos()
        {
            //return;
            //Crear_Packs_Skins(); // Exportar listas de nombres
            Crear_Packs_Skins_Renombrar(); // Renombrar de las listas
            return;
            //Obtener_Nombres_Packs_Recursos();
            //return;
            /*string Texto = null;
            for (int Índice = 1; Índice <= 167; Índice++)
            {
                string Línea = Índice.ToString();
                while (Línea.Length < 10) Línea = '0' + Línea;
                Texto += "Diccionario_Nombres.Add(\"" + Línea + "\", \"\");\r\n";
            }
            Clipboard.SetText(Texto);
            return;*/
            string Ruta_Base = @"C:\Program Files (x86)\Image-Line\FL Studio 9\Data\Projects\__JUPISOFT COMPILATION VOL. 1\Packs";
            if (!string.IsNullOrEmpty(Ruta_Base) && Directory.Exists(Ruta_Base))
            {
                string[] Matriz_Carpetas = Directory.GetDirectories(Ruta_Base, "*", SearchOption.TopDirectoryOnly);
                if (Matriz_Carpetas != null && Matriz_Carpetas.Length > 0)
                {
                    foreach (string Ruta_Entrada in Matriz_Carpetas)
                    {
                        List<string> Lista_Recursos = new List<string>(); //Obtener_Nombres_Packs_Recursos(Ruta_Entrada + "\\a.pck", false);
                        Dictionary<string, string> Diccionario_Nombres = new Dictionary<string, string>();
                        string Pack_Recursos = Path.GetFileNameWithoutExtension(Ruta_Entrada);
                        string[][] Matriz_Objetos = new string[16][]
                        {
                            new string[16] { "leather_helmet", "chainmail_helmet", "iron_helmet", "diamond_helmet", "gold_helmet", "flint_and_steel", "flint", "coal", "string", "seeds_wheat", "apple", "apple_golden", "egg", "sugar", "snowball", "elytra" },
                            new string[16] { "leather_helmet", "chainmail_chestplate", "iron_chestplate", "diamond_chestplate", "gold_chestplate", "bow_standby", "brick", "iron_ingot", "feather", "wheat", "painting", "reeds", "bone", "cake", "slimeball", "broken_elytra" },
                            new string[16] { "leather_leggings", "chainmail_leggings", "iron_leggings", "diamond_leggings", "gold_leggings", "arrow", "end_crystal", "gold_ingot", "gunpowder", "bread", "sign", "door_wood", "door_iron", "bed", "fireball", "chorus_fruit" },
                            new string[16] { "leather_boots", "chainmail_boots", "iron_boots", "diamond_boots", "gold_boots", "stick", "compass", "diamond", "redstone_dust", "clay_ball", "paper", "book_normal", "map_filled", "seeds_pumpkin", "seeds_melon", "chorus_fruit_popped" },
                            new string[16] { "wood_sword", "stone_sword", "iron_sword", "diamond_sword", "gold_sword", "fishing_rod_uncast", "clock", "bowl", "mushroom_stew", "glowstone_dust", "bucket_empty", "bucket_water", "bucket_lava", "bucket_milk", "dye_powder_black", "dye_powder_gray" },
                            new string[16] { "wood_shovel", "stone_shovel", "iron_shovel", "diamond_shovel", "gold_shovel", "fishing_rod_cast", "repeater", "porkchop_raw", "porkchop_cooked", "fish_cod_raw", "fish_cod_cooked", "rotten_flesh", "cookie", "shears", "dye_powder_red", "dye_powder_pink" },
                            new string[16] { "wood_pickaxe", "stone_pickaxe", "iron_pickaxe", "diamond_pickaxe", "gold_pickaxe", "bow_pulling_0", "carrot_on_a_stick", "leather", "saddle", "beef_raw", "beef_cooked", "ender_pearl", "blaze_rod", "melon", "dye_powder_green", "dye_powder_lime" },
                            new string[16] { "wood_axe", "stone_axe", "iron_axe", "diamond_axe", "gold_axe", "bow_pulling_1", "potato_baked", "potato", "carrot", "chicken_raw", "chicken_cooked", "ghast_tear", "gold_nugget", "nether_wart", "dye_powder_brown", "dye_powder_yellow" },
                            new string[16] { "wood_hoe", "stone_hoe", "iron_hoe", "diamond_hoe", "gold_hoe", "bow_pulling_2", "potato_poisonous", "minecart_normal", "oak_boat", "melon_speckled", "spider_eye_fermented", "spider_eye", "potion_bottle_empty", "potion_overlay", "dye_powder_blue", "dye_powder_light_blue" },
                            new string[16] { "leather_helmet_overlay", "spectral_arrow", "iron_horse_armor", "diamond_horse_armor", "gold_horse_armor", "comparator", "carrot_golden", "minecart_chest", "pumpkin_pie", "spawn_egg", "potion_bottle_splash", "ender_eye", "cauldron", "blaze_powder", "dye_powder_purple", "dye_powder_magenta" },
                            new string[16] { "leather_chestplate_overlay", "tipped_arrow_base", "dragon_breath", "name_tag", "lead", "netherbrick", "fish_clownfish_raw", "minecart_furnace", "charcoal", "spawn_egg_overlay", "ruby", "experience_bottle", "brewing_stand", "magma_cream", "dye_powder_cyan", "dye_powder_orange" },
                            new string[16] { "leather_leggings_overlay", "tipped_arrow_head", "potion_bottle_lingering", "barrier", "mutton_raw", "rabbit_raw", "fish_pufferfish_raw", "minecart_hopper", "hopper", "nether_star", "emerald", "book_writable", "book_written", "flower_pot", "dye_powder_silver", "dye_powder_white" },
                            new string[16] { "leather_boots_overlay", "beetroot", "beetroot_seeds", "beetroot_soup", "mutton_cooked", "rabbit_cooked", "fish_salmon_raw", "minecart_tnt", "wooden_armorstand", "fireworks", "fireworks_charge", "fireworks_charge_overlay", "quartz", "map_empty", "item_frame", "book_enchanted" },
                            new string[16] { "door_acacia", "door_birch", "door_dark_oak", "door_jungle", "door_spruce", "rabbit_stew", "fish_salmon_cooked", "minecart_command_block", "acacia_boat", "birch_boat", "dark_oak_boat", "jungle_boat", "spruce_boat", "prismarine_shard", "prismarine_crystals", "potion_bottle_drinkable" },
                            new string[16] { "structure_void", "map_filled_markings", "totem", "shulker_shell", "iron_nugget", "rabbit_foot", "rabbit_hide", "", "", "", "", "", "", "", "", "dragon_fireball" },
                            new string[16] { "record_13", "record_cat", "record_blocks", "record_chirp", "record_far", "record_mall", "record_mellohi", "record_stal", "record_strad", "record_ward", "record_11", "record_wait", "record_13_", "record_cat_", "record_13__", "record_cat__" },
                        };
                        string[][] Matriz_Bloques = new string[32][]
                        {
                            new string[16] { "grass_top", "stone", "dirt", "grass_side", "planks_oak", "stone_slab_side", "stone_slab_top", "brick", "tnt_side", "tnt_top", "tnt_bottom", "web", "flower_rose", "flower_dandelion", "", "sapling_oak" },
                            new string[16] { "cobblestone", "bedrock", "sand", "gravel", "log_oak", "log_oak_top", "iron_block", "gold_block", "diamond_block", "emerald_block", "redstone_block", "dropper_front_horizontal", "mushroom_red", "mushroom_brown", "sapling_jungle", "" },
                            new string[16] { "gold_ore", "iron_ore", "coal_ore", "bookshelf", "cobblestone_mossy", "obsidian", "grass_side_overlay", "tallgrass", "dispenser_front_vertical", "beacon", "dropper_front_vertical", "crafting_table_top", "furnace_front_off", "furnace_side", "dispenser_front_horizontal", "" },
                            new string[16] { "sponge", "glass", "diamond_ore", "redstone_ore", "leaves_oak", "leaves_oak_", "stonebrick", "deadbush", "fern", "daylight_detector_top", "daylight_detector_side", "crafting_table_side", "crafting_table_front", "furnace_front_on", "furnace_top", "sapling_spruce" },
                            new string[16] { "wool_colored_white", "mob_spawner", "snow", "ice", "grass_side_snowed", "cactus_top", "cactus_side", "cactus_bottom", "clay", "reeds", "jukebox_side", "jukebox_top", "waterlily", "mycelium_side", "mycelium_top", "sapling_birch" },
                            new string[16] { "torch_on", "door_wood_upper", "door_iron_upper", "ladder", "trapdoor", "iron_bars", "farmland_wet", "farmland_dry", "wheat_stage_0", "wheat_stage_1", "wheat_stage_2", "wheat_stage_3", "wheat_stage_4", "wheat_stage_5", "wheat_stage_6", "wheat_stage_7" },
                            new string[16] { "lever", "door_wood_lower", "door_iron_lower", "redstone_torch_on", "stonebrick_mossy", "stonebrick_cracked", "pumpkin_top", "netherrack", "soul_sand", "glowstone", "piston_top_sticky", "piston_top_normal", "piston_side", "piston_bottom", "piston_inner", "pumpkin_stem_disconnected" },
                            new string[16] { "rail_normal_turned", "wool_colored_black", "wool_colored_gray", "redstone_torch_off", "log_spruce", "log_birch", "pumpkin_side", "pumpkin_face_off", "pumpkin_face_on", "cake_top", "cake_side", "cake_inner", "cake_bottom", "mushroom_block_skin_red", "mushroom_block_skin_brown", "pumpkin_stem_connected" },
                            new string[16] { "rail_normal", "wool_colored_red", "wool_colored_pink", "repeater_off", "leaves_spruce", "leaves_spruce_", "bed_feet_top", "bed_head_top", "melon_side", "melon_top", "cauldron_top", "cauldron_inner", "sponge_wet", "mushroom_block_skin_stem", "mushroom_block_inside", "vine" },
                            new string[16] { "lapis_block", "wool_colored_green", "wool_colored_lime", "repeater_on", "glass_pane_top", "bed_feet_end", "bed_feet_side", "bed_head_side", "bed_head_end", "log_jungle", "cauldron_side", "cauldron_bottom", "brewing_stand_base", "brewing_stand", "endframe_top", "endframe_side" },
                            new string[16] { "lapis_ore", "wool_colored_brown", "wool_colored_yellow", "rail_golden", "redstoneDust_cross", "redstoneDust_line", "enchanting_table_top", "dragon_egg", "cocoa_stage_2", "cocoa_stage_1", "cocoa_stage_0", "emerald_ore", "trip_wire_source", "trip_wire", "endframe_eye", "end_stone" },
                            new string[16] { "sandstone_top", "wool_colored_blue", "wool_colored_light_blue", "rail_golden_powered", "redstoneDust_cross_overlay", "redstoneDust_line_overlay", "enchanting_table_side", "enchanting_table_bottom", "", "itemframe_background", "flower_pot", "comparator_off", "comparator_on", "rail_activator", "rail_activator_powered", "quartz_ore" },
                            new string[16] { "sandstone_normal", "wool_colored_purple", "wool_colored_magenta", "rail_detector", "leaves_jungle", "leaves_jungle_", "planks_spruce", "planks_jungle", "carrots_stage_0", "carrots_stage_1", "carrots_stage_2", "carrots_stage_3", "slime", "", "", "" },
                            new string[16] { "sandstone_bottom", "wool_colored_cyan", "wool_colored_orange", "redstone_lamp_off", "redstone_lamp_on", "stonebrick_carved", "planks_birch", "anvil_base", "anvil_top_damaged_1", "quartz_block_chiseled_top", "quartz_block_lines_top", "quartz_block_top", "hopper_outside", "rail_detector_powered", "", "" },
                            new string[16] { "nether_brick", "wool_colored_silver", "nether_wart_stage_0", "nether_wart_stage_1", "nether_wart_stage_2", "sandstone_carved", "sandstone_smooth", "anvil_top_damaged_0", "anvil_top_damaged_2", "quartz_block_chiseled", "quartz_block_lines", "quartz_block_side", "hopper_inside", "", "", "" },
                            new string[16] { "destroy_stage_0", "destroy_stage_1", "destroy_stage_2", "destroy_stage_3", "destroy_stage_4", "destroy_stage_5", "destroy_stage_6", "destroy_stage_7", "destroy_stage_8", "destroy_stage_9", "hay_block_side", "quartz_block_bottom", "hopper_top", "hay_block_top", "", "" },

                            new string[16] { "coal_block", "hardened_clay", "noteblock", "stone_andesite", "stone_andesite_smooth", "stone_diorite", "stone_diorite_smooth", "stone_granite", "stone_granite_smooth", "potatoes_stage_0", "potatoes_stage_1", "potatoes_stage_2", "potatoes_stage_3", "log_spruce_top", "log_jungle_top", "log_birch_top" },
                            new string[16] { "hardened_clay_stained_black", "hardened_clay_stained_blue", "hardened_clay_stained_brown", "hardened_clay_stained_cyan", "hardened_clay_stained_gray", "hardened_clay_stained_green", "hardened_clay_stained_light_blue", "hardened_clay_stained_lime", "hardened_clay_stained_magenta", "hardened_clay_stained_orange", "hardened_clay_stained_pink", "hardened_clay_stained_purple", "hardened_clay_stained_red", "hardened_clay_stained_silver", "hardened_clay_stained_white", "hardened_clay_stained_yellow" },
                            new string[16] { "glass_black", "glass_blue", "glass_brown", "glass_cyan", "glass_gray", "glass_green", "glass_light_blue", "glass_lime", "glass_magenta", "glass_orange", "glass_pink", "glass_purple", "glass_red", "glass_silver", "glass_white", "glass_yellow" },
                            new string[16] { "glass_pane_top_black", "glass_pane_top_blue", "glass_pane_top_brown", "glass_pane_top_cyan", "glass_pane_top_gray", "glass_pane_top_green", "glass_pane_top_light_blue", "glass_pane_top_lime", "glass_pane_top_magenta", "glass_pane_top_orange", "glass_pane_top_pink", "glass_pane_top_purple", "glass_pane_top_red", "glass_pane_top_silver", "glass_pane_top_white", "glass_pane_top_yellow" },
                            new string[16] { "double_plant_fern_top", "double_plant_grass_top", "double_plant_paeonia_top", "double_plant_rose_top", "double_plant_syringa_top", "flower_tulip_orange", "double_plant_sunflower_top", "double_plant_sunflower_front", "log_acacia", "log_acacia_top", "planks_acacia", "leaves_acacia", "leaves_acacia_", "prismarine_bricks", "red_sand", "red_sandstone_top" },
                            new string[16] { "double_plant_fern_bottom", "double_plant_grass_bottom", "double_plant_paeonia_bottom", "double_plant_rose_bottom", "double_plant_syringa_bottom", "flower_tulip_pink", "double_plant_sunflower_bottom", "double_plant_sunflower_back", "log_big_oak", "log_big_oak_top", "planks_big_oak", "leaves_big_oak", "leaves_big_oak_", "prismarine_dark", "red_sandstone_bottom", "red_sandstone_normal" },
                            new string[16] { "flower_allium", "flower_blue_orchid", "flower_houstonia", "flower_oxeye_daisy", "flower_tulip_red", "flower_tulip_white", "sapling_acacia", "sapling_roofed_oak", "coarse_dirt", "dirt_podzol_side", "dirt_podzol_top", "leaves_birch", "leaves_birch_", "prismarine_rough", "red_sandstone_carved", "red_sandstone_smooth" },
                            new string[16] { "door_acacia_upper", "door_birch_upper", "door_dark_oak_upper", "door_jungle_upper", "door_spruce_upper", "chorus_flower", "chorus_flower_dead", "chorus_plant", "end_bricks", "grass_path_side", "grass_path_top", "barrier", "ice_packed", "sea_lantern", "daylight_detector_inverted_top", "iron_trapdoor" },
                            new string[16] { "door_acacia_lower", "door_birch_lower", "door_dark_oak_lower", "door_jungle_lower", "door_spruce_lower", "purpur_block", "purpur_pillar", "purpur_pillar_top", "end_rod", "magma", "nether_wart_block", "red_nether_brick", "frosted_ice_0", "frosted_ice_1", "frosted_ice_2", "frosted_ice_3" },
                            new string[16] { "beetroots_stage_0", "beetroots_stage_1", "beetroots_stage_2", "beetroots_stage_3", "chain_command_block_back", "chain_command_block_conditional", "chain_command_block_front", "chain_command_block_side", "command_block_back", "command_block_conditional", "command_block_front", "command_block_side", "repeating_command_block_back", "repeating_command_block_conditional", "repeating_command_block_front", "repeating_command_block_side" },
                            new string[16] { "bone_block_side", "bone_block_top", "melon_stem_disconnected", "melon_stem_connected", "observer_front", "observer_side", "observer_back", "observer_back_lit", "observer_top", "", "", "structure_block", "structure_block_corner", "structure_block_data", "structure_block_load", "structure_block_save" },
                            new string[16] { "concrete_black", "concrete_blue", "concrete_brown", "concrete_cyan", "concrete_gray", "concrete_green", "concrete_light_blue", "concrete_lime", "concrete_magenta", "concrete_orange", "concrete_pink", "concrete_purple", "concrete_red", "concrete_silver", "concrete_white", "concrete_yellow" },
                            new string[16] { "concrete_powder_black", "concrete_powder_blue", "concrete_powder_brown", "concrete_powder_cyan", "concrete_powder_gray", "concrete_powder_green", "concrete_powder_light_blue", "concrete_powder_lime", "concrete_powder_magenta", "concrete_powder_orange", "concrete_powder_pink", "concrete_powder_purple", "concrete_powder_red", "concrete_powder_silver", "concrete_powder_white", "concrete_powder_yellow" },
                            new string[16] { "glazed_terracotta_black", "glazed_terracotta_blue", "glazed_terracotta_brown", "glazed_terracotta_cyan", "glazed_terracotta_gray", "glazed_terracotta_green", "glazed_terracotta_light_blue", "glazed_terracotta_lime", "glazed_terracotta_magenta", "glazed_terracotta_orange", "glazed_terracotta_pink", "glazed_terracotta_purple", "glazed_terracotta_red", "glazed_terracotta_silver", "glazed_terracotta_white", "glazed_terracotta_yellow" },
                            new string[16] { "shulker_top_white", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
                            new string[16] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
                        };

                        byte[] Matriz_Bytes_chain_command_block_back = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_chain_command_block_conditional = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_chain_command_block_front = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_chain_command_block_side = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_command_block_back = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_command_block_conditional = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_command_block_front = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_command_block_side = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_fire_layer_0 = new byte[356] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 49, 54, 44, 10, 32, 32, 32, 32, 32, 32, 49, 55, 44, 10, 32, 32, 32, 32, 32, 32, 49, 56, 44, 10, 32, 32, 32, 32, 32, 32, 49, 57, 44, 10, 32, 32, 32, 32, 32, 32, 50, 48, 44, 10, 32, 32, 32, 32, 32, 32, 50, 49, 44, 10, 32, 32, 32, 32, 32, 32, 50, 50, 44, 10, 32, 32, 32, 32, 32, 32, 50, 51, 44, 10, 32, 32, 32, 32, 32, 32, 50, 52, 44, 10, 32, 32, 32, 32, 32, 32, 50, 53, 44, 10, 32, 32, 32, 32, 32, 32, 50, 54, 44, 10, 32, 32, 32, 32, 32, 32, 50, 55, 44, 10, 32, 32, 32, 32, 32, 32, 50, 56, 44, 10, 32, 32, 32, 32, 32, 32, 50, 57, 44, 10, 32, 32, 32, 32, 32, 32, 51, 48, 44, 10, 32, 32, 32, 32, 32, 32, 51, 49, 44, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 52, 44, 10, 32, 32, 32, 32, 32, 32, 53, 44, 10, 32, 32, 32, 32, 32, 32, 54, 44, 10, 32, 32, 32, 32, 32, 32, 55, 44, 10, 32, 32, 32, 32, 32, 32, 56, 44, 10, 32, 32, 32, 32, 32, 32, 57, 44, 10, 32, 32, 32, 32, 32, 32, 49, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 49, 44, 10, 32, 32, 32, 32, 32, 32, 49, 50, 44, 10, 32, 32, 32, 32, 32, 32, 49, 51, 44, 10, 32, 32, 32, 32, 32, 32, 49, 52, 44, 10, 32, 32, 32, 32, 32, 32, 49, 53, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_fire_layer_1 = new byte[21] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 125, 10, 125 };
                        byte[] Matriz_Bytes_lava_flow = new byte[44] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 51, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_lava_still = new byte[426] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 50, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 52, 44, 10, 32, 32, 32, 32, 32, 32, 53, 44, 10, 32, 32, 32, 32, 32, 32, 54, 44, 10, 32, 32, 32, 32, 32, 32, 55, 44, 10, 32, 32, 32, 32, 32, 32, 56, 44, 10, 32, 32, 32, 32, 32, 32, 57, 44, 10, 32, 32, 32, 32, 32, 32, 49, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 49, 44, 10, 32, 32, 32, 32, 32, 32, 49, 50, 44, 10, 32, 32, 32, 32, 32, 32, 49, 51, 44, 10, 32, 32, 32, 32, 32, 32, 49, 52, 44, 10, 32, 32, 32, 32, 32, 32, 49, 53, 44, 10, 32, 32, 32, 32, 32, 32, 49, 54, 44, 10, 32, 32, 32, 32, 32, 32, 49, 55, 44, 10, 32, 32, 32, 32, 32, 32, 49, 56, 44, 10, 32, 32, 32, 32, 32, 32, 49, 57, 44, 10, 32, 32, 32, 32, 32, 32, 49, 56, 44, 10, 32, 32, 32, 32, 32, 32, 49, 55, 44, 10, 32, 32, 32, 32, 32, 32, 49, 54, 44, 10, 32, 32, 32, 32, 32, 32, 49, 53, 44, 10, 32, 32, 32, 32, 32, 32, 49, 52, 44, 10, 32, 32, 32, 32, 32, 32, 49, 51, 44, 10, 32, 32, 32, 32, 32, 32, 49, 50, 44, 10, 32, 32, 32, 32, 32, 32, 49, 49, 44, 10, 32, 32, 32, 32, 32, 32, 49, 48, 44, 10, 32, 32, 32, 32, 32, 32, 57, 44, 10, 32, 32, 32, 32, 32, 32, 56, 44, 10, 32, 32, 32, 32, 32, 32, 55, 44, 10, 32, 32, 32, 32, 32, 32, 54, 44, 10, 32, 32, 32, 32, 32, 32, 53, 44, 10, 32, 32, 32, 32, 32, 32, 52, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125 };
                        byte[] Matriz_Bytes_magma = new byte[118] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 56, 44, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 50, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_portal = new byte[21] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 125, 10, 125 };
                        byte[] Matriz_Bytes_prismarine_rough = new byte[291] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 51, 48, 48, 44, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 51, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_repeating_command_block_back = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_repeating_command_block_conditional = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_repeating_command_block_front = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_repeating_command_block_side = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_sea_lantern = new byte[52] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 53, 10, 32, 32, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_water_flow = new byte[21] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 125, 10, 125 };
                        byte[] Matriz_Bytes_water_still = new byte[44] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 50, 10, 32, 32, 125, 10, 125, 10 };

                        byte[] Matriz_Bytes_Frametime_1 = new byte[52] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 10, 32, 32, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_Frametime_2 = new byte[52] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 50, 10, 32, 32, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_Frametime_3 = new byte[52] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 51, 10, 32, 32, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_Frametime_4 = new byte[52] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 52, 10, 32, 32, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_Frametime_5 = new byte[52] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 53, 10, 32, 32, 32, 32, 125, 10, 125, 10 };
                        byte[] Matriz_Bytes_Frametime_10 = new byte[53] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 32, 32, 125, 10, 125, 10 };

                        byte[] Matriz_Bytes_Animación_Capicua_4 = new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 };

                        string[] Matriz_Rutas = Directory.GetFiles(Ruta_Entrada, "*.png", SearchOption.TopDirectoryOnly);
                        if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                        {
                            Array.Sort(Matriz_Rutas);
                            if (Lista_Recursos.Count != Matriz_Rutas.Length) MessageBox.Show(Lista_Recursos.Count.ToString() + " y " + Matriz_Rutas.Length.ToString(), Path.GetFileName(Ruta_Entrada));
                            bool Animaciones_Xbox = true; // Usar 4 animaciones más
                            string Ruta_Salida = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Packs\\" + Pack_Recursos + "\\assets\\minecraft\\textures";
                            //foreach (string Ruta in Matriz_Rutas)
                            for (int Índice_Ruta = 0; Índice_Ruta < Matriz_Rutas.Length; Índice_Ruta++)
                            {
                                FileStream Lector = new FileStream(Matriz_Rutas[Índice_Ruta], FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                                Image Imagen_Original = null;
                                try { Imagen_Original = Image.FromStream(Lector, false, false); }
                                catch { Imagen_Original = null; }
                                if (Imagen_Original != null)
                                {
                                    int Ancho_Original = Imagen_Original.Width;
                                    int Alto_Original = Imagen_Original.Height;
                                    //string Número = Path.GetFileNameWithoutExtension(Matriz_Rutas[Índice_Ruta]);
                                    string Nombre = null;
                                    if (Diccionario_Nombres_Packs_Recursos.ContainsKey(Lista_Recursos[Índice_Ruta]))
                                    {
                                        Nombre = Diccionario_Nombres_Packs_Recursos[Lista_Recursos[Índice_Ruta]];
                                    }
                                    else
                                    {
                                        Nombre = Lista_Recursos[Índice_Ruta];
                                        MessageBox.Show(Nombre, "FALTA: " + Pack_Recursos);
                                    }
                                    int Ancho = Ancho_Original;
                                    int Alto = Alto_Original;
                                    List<string> Lista_Ancho_Alto = new List<string>(new string[]
                                    {
                                        "res/mob/pigzombie.png",
                                        "res/mob/zombie.png",
                                        "res/mob/zombie/husk.png"
                                    }); // Reparar texturas
                                    if (Lista_Ancho_Alto.Contains(Lista_Recursos[Índice_Ruta]))
                                    {
                                        if (Ancho != Alto)
                                        {
                                            Ancho = Math.Max(Ancho, Alto);
                                            Alto = Ancho;
                                        }
                                    }
                                    Bitmap Imagen = new Bitmap(Ancho, Alto, !Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format24bppRgb : PixelFormat.Format32bppArgb);
                                    Graphics Pintar = Graphics.FromImage(Imagen);
                                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho_Original, Alto_Original), new Rectangle(0, 0, Ancho_Original, Alto_Original), GraphicsUnit.Pixel);
                                    Pintar.Dispose();
                                    Pintar = null;
                                    Imagen_Original.Dispose();
                                    Imagen_Original = null;
                                    //else // El resto de texturas
                                    {
                                        //MessageBox.Show(Nombre, Lista_Recursos[Índice_Ruta]);
                                        if (!string.IsNullOrEmpty(Nombre))
                                        {
                                            string Ruta_PNG = Ruta_Salida + "\\" + Nombre + ".png";
                                            Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_PNG));
                                            if (!File.Exists(Ruta_PNG) || Animaciones_Xbox)
                                            {
                                                if (!Image.IsAlphaPixelFormat(Imagen.PixelFormat) || !Verificar_Imagen_32_Bits_Transparente(Imagen)) Imagen.Save(Ruta_PNG, ImageFormat.Png);
                                            }
                                            Ruta_PNG = null;
                                        }
                                    }
                                    if (Lista_Recursos[Índice_Ruta] == "res/items.png") // Objetos 16 x 16
                                    {
                                        for (int Y = 0, Índice = 0; Y < 16; Y++)
                                        {
                                            for (int X = 0; X < 16; X++, Índice++)
                                            {
                                                string Ruta_PNG = Ruta_Salida + "\\items\\" + (!string.IsNullOrEmpty(Matriz_Objetos[Y][X]) ? Matriz_Objetos[Y][X] : "unknown_" + (Índice + 1).ToString()) + ".png";
                                                Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_PNG));
                                                if (!File.Exists(Ruta_PNG) || Animaciones_Xbox)
                                                {
                                                    Bitmap Imagen_Temporal = Imagen.Clone(new Rectangle(X * (Ancho / 16), Y * (Alto / 16), Ancho / 16, Alto / 16), Imagen.PixelFormat);
                                                    if (!Image.IsAlphaPixelFormat(Imagen_Temporal.PixelFormat) || !Verificar_Imagen_32_Bits_Transparente(Imagen_Temporal)) Imagen_Temporal.Save(Ruta_PNG, ImageFormat.Png);
                                                    Imagen_Temporal.Dispose();
                                                    Imagen_Temporal = null;
                                                }
                                                Ruta_PNG = null;
                                            }
                                        }
                                    }
                                    else if (Lista_Recursos[Índice_Ruta] == "res/terrain.png") // Bloques 16 x 32
                                    {
                                        for (int Y = 0, Índice = 0; Y < 32; Y++)
                                        {
                                            for (int X = 0; X < 16; X++, Índice++)
                                            {
                                                string Ruta_PNG = Ruta_Salida + "\\blocks\\" + (!string.IsNullOrEmpty(Matriz_Bloques[Y][X]) ? Matriz_Bloques[Y][X] : "unknown_" + (Índice + 1).ToString()) + ".png";
                                                Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_PNG));
                                                if (!File.Exists(Ruta_PNG) || Animaciones_Xbox)
                                                {
                                                    Bitmap Imagen_Temporal = Imagen.Clone(new Rectangle(X * (Ancho / 16), Y * (Alto / 32), Ancho / 16, Alto / 32), Imagen.PixelFormat);
                                                    if (!Image.IsAlphaPixelFormat(Imagen_Temporal.PixelFormat) || !Verificar_Imagen_32_Bits_Transparente(Imagen_Temporal)) Imagen_Temporal.Save(Ruta_PNG, ImageFormat.Png);
                                                    Imagen_Temporal.Dispose();
                                                    Imagen_Temporal = null;
                                                }
                                                Ruta_PNG = null;
                                            }
                                        }
                                    }
                                    else if (Lista_Recursos[Índice_Ruta] == "res/textures/items/clock.png") // Reloj x 64
                                    {
                                        for (int Y = 0; Y < Alto / Ancho; Y++)
                                        {
                                            string Texto = Y.ToString();
                                            while (Texto.Length < 2) Texto = '0' + Texto;
                                            string Ruta_PNG = Ruta_Salida + "\\" + Nombre + Texto + ".png";
                                            Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_PNG));
                                            if (!File.Exists(Ruta_PNG) || Animaciones_Xbox)
                                            {
                                                Bitmap Imagen_Temporal = Imagen.Clone(new Rectangle(0, Y * Ancho, Ancho, Ancho), Imagen.PixelFormat);
                                                if (!Image.IsAlphaPixelFormat(Imagen_Temporal.PixelFormat) || !Verificar_Imagen_32_Bits_Transparente(Imagen_Temporal)) Imagen_Temporal.Save(Ruta_PNG, ImageFormat.Png);
                                                Imagen_Temporal.Dispose();
                                                Imagen_Temporal = null;
                                            }
                                            Ruta_PNG = null;
                                            Texto = null;
                                        }
                                    }
                                    else if (Lista_Recursos[Índice_Ruta] == "res/textures/items/compass.png") // Brújula x 32
                                    {
                                        for (int Y = 0; Y < Alto / Ancho; Y++)
                                        {
                                            string Texto = Y.ToString();
                                            while (Texto.Length < 2) Texto = '0' + Texto;
                                            string Ruta_PNG = Ruta_Salida + "\\" + Nombre + Texto + ".png";
                                            Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_PNG));
                                            if (!File.Exists(Ruta_PNG) || Animaciones_Xbox)
                                            {
                                                Bitmap Imagen_Temporal = Imagen.Clone(new Rectangle(0, Y * Ancho, Ancho, Ancho), Imagen.PixelFormat);
                                                if (!Image.IsAlphaPixelFormat(Imagen_Temporal.PixelFormat) || !Verificar_Imagen_32_Bits_Transparente(Imagen_Temporal)) Imagen_Temporal.Save(Ruta_PNG, ImageFormat.Png);
                                                Imagen_Temporal.Dispose();
                                                Imagen_Temporal = null;
                                            }
                                            Ruta_PNG = null;
                                            Texto = null;
                                        }
                                    }
                                    if (string.IsNullOrEmpty(Nombre)) MessageBox.Show(Lista_Recursos[Índice_Ruta], Pack_Recursos);
                                    Imagen.Dispose();
                                    Imagen = null;
                                }
                            }
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\chain_command_block_back.png.mcmeta", Matriz_Bytes_chain_command_block_back);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\chain_command_block_conditional.png.mcmeta", Matriz_Bytes_chain_command_block_conditional);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\chain_command_block_front.png.mcmeta", Matriz_Bytes_chain_command_block_front);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\chain_command_block_side.png.mcmeta", Matriz_Bytes_chain_command_block_side);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\command_block_back.png.mcmeta", Matriz_Bytes_command_block_back);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\command_block_conditional.png.mcmeta", Matriz_Bytes_command_block_conditional);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\command_block_front.png.mcmeta", Matriz_Bytes_command_block_front);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\command_block_side.png.mcmeta", Matriz_Bytes_command_block_side);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\fire_layer_0.png.mcmeta", Matriz_Bytes_fire_layer_0);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\fire_layer_1.png.mcmeta", Matriz_Bytes_fire_layer_1);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\lava_flow.png.mcmeta", Matriz_Bytes_lava_flow);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\lava_still.png.mcmeta", Matriz_Bytes_lava_still);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\magma.png.mcmeta", Matriz_Bytes_magma);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\portal.png.mcmeta", Matriz_Bytes_portal);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\prismarine_rough.png.mcmeta", Matriz_Bytes_prismarine_rough);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\repeating_command_block_back.png.mcmeta", Matriz_Bytes_repeating_command_block_back);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\repeating_command_block_conditional.png.mcmeta", Matriz_Bytes_repeating_command_block_conditional);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\repeating_command_block_front.png.mcmeta", Matriz_Bytes_repeating_command_block_front);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\repeating_command_block_side.png.mcmeta", Matriz_Bytes_repeating_command_block_side);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\sea_lantern.png.mcmeta", Matriz_Bytes_sea_lantern);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\water_flow.png.mcmeta", Matriz_Bytes_water_flow);
                            File.WriteAllBytes(Ruta_Salida + "\\blocks\\water_still.png.mcmeta", Matriz_Bytes_water_still);
                            if (Animaciones_Xbox)
                            {
                                if (Pack_Recursos == "Candy")
                                {
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\deadbush.png.mcmeta", Matriz_Bytes_Frametime_4);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\diamond_block.png.mcmeta", Matriz_Bytes_Frametime_5);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\emerald_block.png.mcmeta", Matriz_Bytes_Frametime_5);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\obsidian.png.mcmeta", Matriz_Bytes_Frametime_5);
                                }
                                else if (Pack_Recursos == "Cartoon")
                                {
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\double_plant_sunflower_front.png.mcmeta", Matriz_Bytes_Frametime_4);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\flower_dandelion.png.mcmeta", Matriz_Bytes_Frametime_3);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\flower_rose.png.mcmeta", Matriz_Bytes_Frametime_4);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\mushroom_red.png.mcmeta", Matriz_Bytes_Frametime_5);
                                }
                                else if (Pack_Recursos == "Chinese Mythology")
                                {
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\furnace_front_on.png.mcmeta", Matriz_Bytes_Frametime_4);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\glowstone.png.mcmeta", new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\pumpkin_face_on.png.mcmeta", new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\redstone_lamp_on.png.mcmeta", new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                }
                                else if (Pack_Recursos == "City")
                                {
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\diamond_ore.png.mcmeta", Matriz_Bytes_Frametime_5);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\emerald_ore.png.mcmeta", Matriz_Bytes_Frametime_5);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\redstone_lamp_on.png.mcmeta", new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\redstone_ore.png.mcmeta", Matriz_Bytes_Frametime_5);
                                }
                                else if (Pack_Recursos == "Fallout")
                                {
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\redstone_lamp_on.png.mcmeta", new byte[94] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                }
                                else if (Pack_Recursos == "Fantasy")
                                {
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\endframe_eye.png.mcmeta", new byte[130] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 52, 44, 10, 9, 32, 32, 53, 44, 10, 9, 32, 32, 52, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\glowstone.png.mcmeta", new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\pumpkin_face_on.png.mcmeta", new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\redstone_lamp_on.png.mcmeta", new byte[94] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                }
                                else if (Pack_Recursos == "Festive")
                                {
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\glowstone.png.mcmeta", new byte[144] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 52, 44, 10, 9, 32, 32, 53, 44, 10, 9, 32, 32, 54, 44, 10, 9, 32, 32, 55, 44, 10, 9, 32, 32, 56, 44, 10, 9, 32, 32, 57, 44, 10, 9, 32, 32, 49, 48, 44, 10, 9, 32, 32, 49, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\log_jungle.png.mcmeta", new byte[52] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 53, 10, 32, 32, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\redstone_lamp_on.png.mcmeta", new byte[130] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 52, 44, 10, 9, 32, 32, 53, 44, 10, 9, 32, 32, 52, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\vine.png.mcmeta", new byte[52] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 54, 10, 32, 32, 32, 32, 125, 10, 125, 10 });
                                }
                                else if (Pack_Recursos == "Greek Mythology")
                                {
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\endframe_eye.png.mcmeta", Matriz_Bytes_Frametime_10);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\glowstone.png.mcmeta", new byte[154] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 52, 44, 10, 9, 32, 32, 53, 44, 10, 9, 32, 32, 54, 44, 10, 9, 32, 32, 55, 44, 10, 9, 32, 32, 54, 44, 10, 9, 32, 32, 53, 44, 10, 9, 32, 32, 52, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\redstone_lamp_on.png.mcmeta", new byte[154] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 52, 44, 10, 9, 32, 32, 53, 44, 10, 9, 32, 32, 54, 44, 10, 9, 32, 32, 55, 44, 10, 9, 32, 32, 54, 44, 10, 9, 32, 32, 53, 44, 10, 9, 32, 32, 52, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\soul_sand.png.mcmeta", new byte[246] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 53, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 52, 44, 10, 9, 32, 32, 53, 44, 10, 9, 32, 32, 54, 44, 10, 9, 32, 32, 55, 44, 10, 9, 32, 32, 56, 44, 10, 9, 32, 32, 57, 44, 10, 9, 32, 32, 49, 48, 44, 10, 9, 32, 32, 49, 49, 44, 10, 9, 32, 32, 49, 50, 44, 10, 9, 32, 32, 49, 51, 44, 10, 9, 32, 32, 49, 52, 44, 10, 9, 32, 32, 49, 51, 44, 10, 9, 32, 32, 49, 50, 44, 10, 9, 32, 32, 49, 49, 44, 10, 9, 32, 32, 49, 48, 44, 10, 9, 32, 32, 57, 44, 10, 9, 32, 32, 56, 44, 10, 9, 32, 32, 55, 44, 10, 9, 32, 32, 54, 44, 10, 9, 32, 32, 53, 44, 10, 9, 32, 32, 52, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                }
                                else if (Pack_Recursos == "Halloween")
                                {

                                }
                                else if (Pack_Recursos == "Halloween 2015")
                                {
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\cactus_side.png.mcmeta", new byte[44] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 56, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\flower_dandelion.png.mcmeta", Matriz_Bytes_Frametime_4);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\flower_rose.png.mcmeta", Matriz_Bytes_Frametime_4);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\glowstone.png.mcmeta", Matriz_Bytes_Frametime_4);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\wool_colored_green.png.mcmeta", Matriz_Bytes_Frametime_4);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\wool_colored_pink.png.mcmeta", Matriz_Bytes_Frametime_4);
                                }
                                else if (Pack_Recursos == "Halo")
                                {

                                }
                                else if (Pack_Recursos == "MassEffect")
                                {

                                }
                                else if (Pack_Recursos == "Natural")
                                {
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\endframe_eye.png.mcmeta", new byte[45] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\glowstone.png.mcmeta", new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\pumpkin_face_on.png.mcmeta", new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\redstone_lamp_on.png.mcmeta", new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                }
                                else if (Pack_Recursos == "Pattern")
                                {
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\flower_dandelion.png.mcmeta", Matriz_Bytes_Frametime_4);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\flower_rose.png.mcmeta", Matriz_Bytes_Frametime_4);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\redstone_lamp_on.png.mcmeta", new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\soul_sand.png.mcmeta", Matriz_Bytes_Frametime_4);
                                }
                                else if (Pack_Recursos == "Plastic")
                                {
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\glowstone.png.mcmeta", new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\pumpkin_face_on.png.mcmeta", new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\redstone_lamp_on.png.mcmeta", new byte[106] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });

                                }
                                else if (Pack_Recursos == "Skyrim")
                                {

                                }
                                else if (Pack_Recursos == "Steampunk")
                                {
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\endframe_eye.png.mcmeta", new byte[45] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\glowstone.png.mcmeta", new byte[118] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 9, 32, 32, 49, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 52, 44, 10, 9, 32, 32, 51, 44, 10, 9, 32, 32, 50, 44, 10, 9, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 });
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\log_spruce.png.mcmeta", Matriz_Bytes_Frametime_5);
                                    File.WriteAllBytes(Ruta_Salida + "\\blocks\\redstone_lamp_on.png.mcmeta", Matriz_Bytes_Frametime_10);
                                }
                            }
                        }
                        Matriz_Rutas = null;
                    }
                    Matriz_Carpetas = null;
                }
            }
        }

        /*Stream input_file_f, output_file_f;
        ushort[] buffer, relay_buffer;
        ulong bytes_read;
        int i, size, offset;
        string output_pathA, input_pathA;

        // SeparateX.cpp : Defines the entry point for the console application.
        //

        /* _tmain takes an XBox .mcr file and reads all the compressed chunk blocks into individual files.
         * The method also copies the 4 KB "last updated" header of the .mcr file into a new file called header_output.txt.
         * This header_output.txt will be used by later programs when assembling the final PC .mcr file.

         * _tmain expects two command line arguments:
         * argv[0]: Path to the .mcr file from which the data will be read.
         * argv[1]: Path to the directory into which the files should be output.
         */
        /*
       int _tmain(int argc, string[] argv)
       {

           // Allocate memory for buffer and offsets and sizes
           buffer = new ushort[4096];
           relay_buffer = new ushort[4096];
           input_pathA = new char[200];
           output_pathA = new char[200];

           // Open the file and read in the first 4096.
           sprintf(input_pathA, "%ls", argv[1]);
           input_file_f = fopen(input_pathA, "rb");
           bytes_read = fread(buffer, sizeof(char), 4096, input_file_f);

           // Create the header_output.txt file and write out two blocks: the offset block and last updated block.
           // The offset block will be overwritten by a later program.
           output_file_f = fopen("header_output.txt", "wb");
           bytes_read = fread(relay_buffer, sizeof(char), 4096, input_file_f);
           fwrite(buffer, sizeof(char), 4096, output_file_f);
           fwrite(relay_buffer, sizeof(char), 4096, output_file_f);
           fclose(output_file_f);

           // Now loop through the 1024 chunk entries
           for (i = 0; i < 4096; i += 4)
           {

               // Grab offset and size for each
               offset = buffer[i] * 65536 + buffer[i + 1] * 256 + buffer[i + 2];
               size = buffer[i + 3];

               // If the offset is zero, we do nothing more; this chunk has no associated data.
               if (offset != 0)
               {

                   // Create an output file for this chunk
                   sprintf(output_pathA, "%ls\\%d.dat", argv[2], i / 4);
                   output_file_f = fopen(output_pathA, "wb");

                   // Move the input file pointer to the location of the chunk
                   // The first four bytes are the length of the data; store that first.
                   fseek(input_file_f, 4096 * offset, SEEK_SET);
                   fread(relay_buffer, sizeof(char), 4, input_file_f);
                   offset = ((relay_buffer[1] << 16) + (relay_buffer[2] << 8) + relay_buffer[3]) % 4096;

                   // Relay the input immediately to the output file in 4KB chunks
                   for (int x = 1; x < size; x++)
                   {

                       // Try to read the next segment.
                       bytes_read = fread(relay_buffer, sizeof(char), 4096, input_file_f);
                       if (bytes_read != 4096) printf("Warning: could not read sector %d for chunk %d.\n", x, i / 4);
                       bytes_read = fwrite(relay_buffer, sizeof(char), 4096, output_file_f);
                       if (bytes_read != 4096) printf("Warning: could not write sector %d for chunk %d.\n", x, i / 4);

                   }

                   // Read the final segment without its trailing zeros.
                   bytes_read = fread(relay_buffer, sizeof(char), offset, input_file_f);
                   if (bytes_read != offset) printf("Warning: could not read final sector for chunk %d.\n", i / 4);

                   // Write the final segment to the file and close the handle.
                   fwrite(relay_buffer, sizeof(char), bytes_read, output_file_f);
                   if (bytes_read != offset) printf("Warning: could not write final sector for chunk %d.\n", i / 4);
                   fclose(output_file_f);

               }

           }

           fclose(output_file_f);

           // Clear memory
           delete[4096] buffer;
           delete[200] input_pathA;
           delete[200] output_pathA;
           delete[4096] relay_buffer;

           return 0;

       }*/



















    }
}
