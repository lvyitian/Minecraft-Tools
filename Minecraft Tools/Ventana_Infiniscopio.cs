using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Infiniscopio : Form
    {
        public Ventana_Infiniscopio()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Infiniscope [TOP SECRET] by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch FPS_Cronómetro = Stopwatch.StartNew();
        internal long FPS_Segundo_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        internal bool Ocupado = false;
        internal float Variable_Zoom = 1f;

        private void Ventana_Plantilla_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Zoom: " + Program.Traducir_Número(Variable_Zoom) + "x]";
                this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                float Zoom = Variable_Zoom;
                string Texto_Ayuda = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang3082{\\fonttbl{\\f0\\fnil\\fcharset0 " + Barra_Estado_Etiqueta_Sugerencia.Font.Name + ";}{\\f1\\fnil\\fcharset0 Calibri;}}\r\n{\\*\\generator Riched20 6.3.9600}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs" + (10 * 2).ToString() + " " +
                "\\ul \\b [Infiniscope... what's this?]\\b0 \\ulnone \\par\\par\r\n" +
                "\\b - \"Disclaimer\":\\b0  this is not Minecraft related, if you're not interested in an ancient alien telescope or don't want to know our past as humanity, please don't read this. But if you read it, please don't assume that's true because I say it, I might be wrong on some parts or all of them, so please have an open mind and judge by yourself carefully what I say, thanks in advance.\\par\\par\r\n" +
                "\\b - \"Description of the actual telescope problem\":\\b0  First of all don't believe anything of what I say here, just judge by yourself. We all know that today's telescopes and microscopes, specially the first ones, need to be bigger every time in order to magnify even more than before. So with this \"obsolete\" method eventually we might end up with a telescope or lens bigger than our own planet one day. And although this may sound ridiculous, using the actual telescope system one day we may get there, or at least found ourselves that we can't see futher away due to this technical problem of the physical size of our telescopes.\\par\\par\r\n" +
                "\\b - \"A problem solved thousands of years ago\":\\b0  this will sound to science fiction for most people, and perhaps will be better that way, at least while they aren't ready for the truth. More than ten thousand years ago, we have some \"legends\" about the Atlantis, the universal flood, and some other things like that, written even in our Bible. Perhaps you heard sometime about a novel made by Jonathan Swift called \"Gulliver's travels\", where on it's third chapter it mentions a floating city called \"Laputa\", whose people where always looking worried at the skies. In that novel it's mentioned that those people had portable telescopes capable of magnifying a lot more than our biggest telescopes, and they even had discovered and mastered anti-gravity. Of course almost everybody will see this novel as a fiction, and maybe by some like the novels of Jules Verne, a source of inspiration for future devices or inventions.\\par\\par\r\n" +
                "\\b - \"Infiniscope\":\\b0  it's a portable device made of a few parts capable of magnifying literally to infinite as telescope or microscope. It's main feature is that it filters the light several times (maybe even millions of times), but since light goes literally at light speed, then it only takes a fraction of second to do it. So basically a modern telescope filters only once the light, so it needs a bigger to lens the further we want to see or focus. But this device makes the light iterate in a closed circuit for a period of time, magnifying it on each pass a certain amount, which depends on the lens, so it sacrifices just a few milliseconds of wait to zoom more than a telescope bigger than our planet.\\par\\par\r\n" +
                "\\b - \"It's parts\":\\b0  the first part is some sort of mixed crystal (but not glass exactly), on it's exterior side it has to be polarizable if it receives electricity, avoiding or letting the exterior light get inside. On the interior side it has to be like a perfect mirror, so it reflects without deviations the light, even after millions of iterations of the same ray of light. The light diesn't fade out like the sound, althoguh colliding with imperfect surfaces like our current lenses or mirrors will make it fade out eventually, or at least be out of focus and very dispersed. But if let's suppose we could design a perfect lens and mirror and make the light cycle, turning 90 degrees four times in a loop, but without loss, then we could make that ray of light loop forever without fading out. And that's exactly what it does that machine. First de-polarizes the external mirror, so a single ray of exterior light can get it. Almost after that, in the same moment it closes again the external mirror by polarizing it, so no more light can enter. While this is done the light that entered before passes through a single and perfect lens, which focus and magnifies it a bit, then reflects itself 90 degrees on a mirror, then a second, a third and finally of the same were the light entered, which is polarized, so it acts as a mirror also when closed. After that the light goes back again to the same lens that before and the circuit repeats as much time as needed. here the time is what determines the distance to zoom or focus. This time has to be perfectly controlled by a super fast chipset or everything will fail. Once the amount of time r estimated loops have occured, the seconf mirror, that it's after the lens, gets de-polarized, letting the light through it to a sensor, which will need different sensibilities depending on what one wants to see. Then simply send the image recorded to any regular computer and you'll have the best portable telescope / microscope in the world. Note: it's also possible to make it using only 2 or 3 mirrors and reflecting the light 120 or 180 degrees, but with 2 mirror it would need a double lens, which if done well might be faster. Shouldn't be too expensive to create this device, althoguh I have no founds or means to make it, which is a shame, but perhaps someone out there could lend me a hand to make it real again. If so, please get in contact sending a mail to Jupitermauro@gmail.com and I might give you more detailed instructions on how to make it. With this you could literally see the atoms as a microscope or even see an alien walking on a distant planet as a telescope (if it was, of course). Sadly I'm not sure that to the interests of today's powers this device would be well received (I hope it never gets used in anything related to wars or destruction), so it's more than possible it will never see the light (pun intended). But well, perhaps in a future live it will can...\\par\\par\r\n" +

                "\\ul \\b [The true floating city]\\b0 \\ulnone \\par\\par\r\n" +
                "\\b - \"Remembering past lives\":\\b0  I've been always connected to technology and inventions, although I've always lived behind the others into access to technology. And I've never read the Gulliver's novels ever before. Literally the only references that I managed to remember was the Gulliver tunnel in the Doraemon cartoon and the Lilliput people mentioned in Gravity Falls, in the golf episode. From time to time I like to watch some mystery videos about ancient enigmas or our forgotten history. But I'm never sure about anything mentioned in any of the videos, but even more than before I wanted to know the truth about everything. And I made up my mind very strongly about knowing the truth about our past as humanity. I really wanted to know a way to see the pure truth between all this confusion and lies. And a few days later I had the most realistic \"dream\" in my whole live.\\par\\par\r\n" +
                "\\b - \"Before the vision\":\\b0  suddenly I appeared behind my body (although I couldn't see it well, it was like in shadow, but it wasn't the body I have in my current live). It was like being in spectator mode in Minecraft, the camera gets so close to the body and suddenly I'm inside and the vision starts, seeing it from the eyes of that body. I was living in a floating city, thousands of years ago. We had a war between another faction or race (which I couldn't see) and I remember the memories from that life or whatever it was. The people from that city were from another planet (not from the Earth), but I believe that from our own Solar system, near Mars (or from it perhaps?). I remember a war and a planet that exploded. In that moment I (or at least the body from that vision) was living inside a fully equiped space station, thousands of times better than our current international space station. It was fully reliable and literally perfect. While working there monitoring the solar system and the live development through it, the war arrived to it's maximum point and our supposed home planet was destroyed. I wasn't there in the vision, but probably thousands or millions of people would have died. Without direct orders, it was a \"run for it\" and I remember arriving to Earth there was a lot of other people, but strangely enough all the other people from my vision was \"censored\", so I never had a chance to see any other people in it, although it was clear that were a lot more people near me all the time. At our arrival we only had the technology we had in the space station or outside of the destroyed planet, so most of it was lost forever within the war. With the remaining technology and equipment, we managed to develop a lot of infrastructures here on Earth, I remember it was something like when conquering the New world (America), the world was split into several zones. And on top of all of them a floating city was build to rule them all. It was the perfect place to monitor the rest of places in the planet. I remeber all the secrets about the construction of the floating city, but I don't remember the name it had in that time, or the name of any device or machine, only the steps to reproduce it back today, which is kinda strange.\\par\\par\r\n" +
                "\\b - \"The floating city\":\\b0  it was literally huge. I don't remeber it's exact size, but walking around it's outside will take a few days, at least, to return to the starting point. It's base was made of huge hexagonal blocks, but plain on the top and bottom, like hexagonal cilinders. Along the sides of every hexagonal block there were 2 rails or kinda metal lines, bright like something chromed, that touched the rails of other hexagonal blocks when put near anothers. The hexagonal blocks were almost dark in color, and made of the strongest material in the known universe. I can't remember the exact material, but they were harder than diamond, and on it's outer side they had some bright almost like glass. Some they were black like coal but shiny like glass almost and harder than diamonds. This should give enough clues of the material, but I'm not sure about it's name. The hundreds of hexagonal blocks (every block was at least several meters of big, maybe even a few dozens), and all of them were perfectly cutted, like from a molten material they were atomically perfectly cutted. And when put near other blocks they appear to the eye as only one giant block. It was also the central hexagonal block, which was smelted of fused with the main anti-gravity machine, so it was all one piece. I remeber from there that gravity doesn't depend on mass like I was told on school, but on acceleration. So if you had particles running at their default speed, of course if you have a lot of them there will be more attraction, but if you manage to modify it's acceleration, you'll end up modifying it's gravitational field, thus achieving controlled anti-gravity, even in a portable way, so better than jetpacks, or in a massive way, like the whole floating city. The machine had a resonance system of infinite energy, starting from a tiny vibration, perfectly reverbered in a kind of \"pyramidal\" shape, it ended up in having more energy returned than the initially spend at the start, so it meant that the main machine will never run out of energy, so it was perfectly designed to never fail, not like our \"modern\" machines, which only last for a few years. in that old time the things lastest for thousands of years or even more, because it was not about the money, but perfection and security. So once the main anti-gravity block was \"on\" the other nearby placed hexagonal blocks acted like a magnet and perfectly fit as one piece. The floating city was created, or at least it's base, which had some \"carvings\" or \"caves\" with galleries inside to move between the hexagonal blocks as a complex system. On it's center it was the anti-gravity machine, perfectly isolated from the outside, and heavily guarded by the \"technics\" or main \"astronomers\" working on it and giving it orders to move to any place of the planet. Not even the regular people of the city could enter the main machine for security reasons. Only a few specialized people had access there, and in the vision I was one of them.\\par\\par\r\n" +
                "\\b - \"The full vision\":\\b0  I was on the south-west zone of the floating city, near one of it's precipices, which didn't had any safe protection to avoid falling off of it, looknig down at the rest of the Earth. I'm not sure what place I was looking at, but I saw 2 rivers and some high mountains at the sides of the rivers, which were close to the other. I couldn't see much vegetation, it looked more like some kind of savanna, but almost without trees or almost a desert, although I saw 2 rivers with blue water, and by the way the sky was blue, so I assumed I was on the Earth. The sensation in that moment is the best I've ever experienced even in my actual whole live. It was like being in harmony with the whole universe, vibrating as one. there was no worries, pain, or anything bad, only harmony. The city was moving to some place, but the movement wasn't noticeable being inside. The movement was made with the 2 previoulsy mentioned rails, the anti-gravity machine was sending some kind of magnetic electricity, that turned the rails into magnet of opposite attraction, but it could also modify the quantity of energy it was sending, resulting in one of the rails trying to move towards the other, but as they were enclosed by harder than diamond black hexagoanl blocks, the resuklt of that force was even making more force than the Earth gravity, and being able to fly at will in a very precised and controlled way, and to select a direction, it sent more energy to one side than the other and it was like \"oscillating\" a bit up and down and moving that way in a 3D direction or vector. I remember my feelings there, I was very happy to live in that city, and I loved it very much. But something that was censored from the vision happenend, the story suddenly moved on. i remember that the people of that city had done something, something bad. I'm not sure what it was. But some kind of civil war started at the floating city. Suddenly I was having some kind of weapon in my hands, some sort of 2 handed weapon like a rifle, but white in color, and very different from our current rifles. It didn't shot bullets, but energy. Luckily I never used against anything as far as I remember. I started the vision at the south-east. At the south it was an octagonal giant temple with giant arches on it's walls, it was made of something like marble or quartz, very white. By the way any building on top of the floating city was always of that material, white in color, but the base was always black like coal and always plain. At the west there was the houses for the people, sort of cilindrical or semi-spherical. None of the buildings had any edge, so even if one falls and hits something there will be no major harm, it was a great improvement in security. At the north there was the main palace, of the ruler of the city, which I never arrived to see. At the east it was the UFO parking, or at least vehicles in a shape like 2 plates reversed, like a classic UFO, they were anti-gravity vehicles, with the same physics that the floating city itself. So the city, might look like a mother ship, but it wasn't at least for the people there. Also it was curious that those vehicles were always on, even parked they floated a few over the ground and were ready to go full speed in case of emergency. Today this will look like very contaminating, etc, but those machines didn't emitted anything and had infinite energy thanks to the resonance effect applied to anti-gravity. When I had the rifle in my hands I suddenly was at the east, near the UFOs (but the perception was like our today's cars and not space ships). and I went to the center zone, were the anti-gravity machine was. Near there I met with 2 people that I could see they weren't from the floating city, i'm not sure how they arrived there, and were the only people I saw in the whole vision. they were transporting a big package, it looked very heavy, or at least big. I was the only one of the 3 that could allow access to the main anti-gravity machine, and I did opened the security door, they entered, but I didn't. One of them closed the door from inside, blocking it even for me. Then they started to manipulate the package for almost five minutes, near the anti-gravity machine. After the vision my first impression was that we were on war again and I was trying to activate some sort of shield to protect the main machine, but the way it was built it was already attack proof, some a few days later it looked more of an attack to my own city. Once they finished installing something, one of them made me a sign with it's hand, so I turned around and started running as fast as I could after leaving my weapon near my back. I ran towards the UFOs, I entered into one and I ran below the floating city, on it's east side. I saw the Earth below again, but this time there was no land nearby, only an infinite ocean, I turned the ship into the south-west direction, almost passing below the point were I started the vision, and actually going into that same direction I was looking, although it wasn't the same position of the Earth, it was only ocean, so or it was flooded or it was another point. I ran for a few seconds (maybe a minute), and finally I saw terrain below, it was a massive jungle without nearby civilizations. Suddenly I felt like the UFO was hit by something, the power turned off and it begin a crashing trajectory towards that jungle. Everything went black for a few seconds or minutes. The next thing I remember was that i survuived the crash, I excited the UFO and I started crossing the jungle, even leaving my weapon. I looked at the sky and when I saw in the distance the floating city, i ran as fast as I could on foot in the opposite direction, trying to scape of the city. From time to time I looked a moment behind me to see if the city was still there, but a fter a few time doing this while running between trees that obstructed my vision and avoiding big bushes, plants and rocks I turned back and suddenly a flash of light brighter than the sun appeared in the middle of the distant city. In less than a few seconds it fully covered, it the whole sky and arrived where I was. Everytihng turned pure white, then pure black. I was dead and the vision ended abruptly, luckily I didn't had time to experience any pain it was instantly, but what wass that? So I would assume that some kind of nuclear bomb exploded in the heart of the city. The hexagonal blocks were nuclear proof, but not the anti-gravity machine, so if that package was a bomb, the city shouldn't have survived, neither (all?) of it's people. I couldn't see any red, orange or yellow color of explosion, just pure white light and that's it, so if that light killed me kilometers away, the anti-gravity machine should have been destroyed, meaning that all the hexagonal blocks would have separated and the whole city ended up in the bottom of the ocean, although the hexagonal blocks shouldn't have been destroyed, the rest should, so between 1/4 and 1/3 of it should have survived, but the interior and exterior no, only the base. So far it has been the more realistic dream I ever had, and I could remember how all the machines worked. Sadly it would be too dangerous to recreate that technology today, but i suppose that an infiniscope shouldn't be too dangerous. I googled hundreds of times about floating cities after the vision, and I had no luck until I found a picture that was almost like I saw, then I read the article and it talked about the Gulliver novel, which I dind't know existed before. If I had known the novel before, I would have thought that the vision came from it, luckily I know I can't explain it, only learn from it. I've tried to place that events in history, and it almost fits in the Atlantis and universal flood. Plato wrote about it, an event from almost 13.000 years ago, the so called \"gods\" or fallen angels came to Earth, and in many cultures they lived on the \"sky\", Olimpus, etc. Also there are some curious similar words like \"Atlantis\" and \"Atlantic\" ocean, or from the basque people in the north of Spain, centuries ago thtat place was called \"Biscaia\" in some ancient maps, which in spanish would come from \"bizco\" that means \"cross-eyed\", and it's very weird because on the Gulliver novel it mentions that the people of that floating city were like that, so it might not actually be a joke but a fact. Also it seems that most people from that zone have the blood type RH -, which also had the pharaohs of Egypt, etc. So could this people come from Atlantis itself, it's latest survivors? By the way I'm RH - myself, although I'm not from that zone, for sure some ancestors had to be from there. Also I have inherited some physical differences with other people, like having 2 cervical vertebrae fused together, which makes my head unable to stand straight without holding it with my hand most part of the day, I even remember that my teachers at school called my attention about it thinking I was bored in class, but I couldn't explain it myself until I had some medical exam years later. I also have six nails on each feet, but only 5 fingers, but my little fingers are the double in size from it's neighbour with 2 nails touching each other, one a bit bigger than the other, but without separation between them, which is a nightmare to clean properly, although luckily it never hurted me, so I just ignored it, which is weird, so possibly it has to do with some strange DNA that had 6 fingers in the past, like in Gravity Falls. I never had any paranormal experience or so, although I can remember some weird things that I believe are from previous lives. Also note that within the basque people today there are places around called \"Lapurdi\" or \"Benabarre\", strangely similar to \"Laputa\" or \"Balnibarbi\" from the Gulliver's travels, and they also sound like italian, so clearly there is an unstudied relation between all this. Could this be a floating city, that was destroyed, perhaps because the people there destroyed the humans below, which lead to an internal war and to destruction of most ancient technology and knowledge? For some weeks I thought that this city might've been Atlantis, but after reading it's description made by Plato (the original text translated), now I believe it wasn't, although the floating city could have been half or fullt submerged without problem while working fine, it could have been disguised like a regular island without problem, which makes me think of \"Hy-Brasil\", word that resembles to \"Yggdrasil\", I suppose that's just another coincidence, although I know that real randomness doesn't exist since all it mathematic, and thus predictable, even the past and future. But for sure it was develpoed using the same methods or machines, which makes me think also of the \"god\" Poseidon, that had overpowered technolgy to terraform Atlantis itself. I believe that most of these overpowered people lived on the floating city, and the words at the end of the text by Plato where he reunites all the gods in a single room where all the Earth can be seen, reminds me of that floating city also. After that vision I can't avoid feeling trapped in a prehistoric place or \"hell\", almost without technology, at least how the one I saw. At least I know what will be possible to achieve again some day, hopefully this time without any destruction or war. But of course feel free to have your own conclusions and investigate by yourself. Also note that the international space station have a strange hexagonal window... very strange don't you think? But also today they are creating a space nation called \"Asgardia\", whose icon or seal is a circle made of hexagons, not to mention the Eye of Horus it has on one side... is this for real? It seems the case, so obviously someone else managed to remember this floating city, and they are trying to rebuild it today... honestly I don't know what to say... hopefully this time will end well. I have no hate against anything or anyone, I only hope to live my life in peace and help the others as much as I can, even for free if I have the resources to do it, so one day (for real) this world will be a place deserved to be called \"home\", for all of us. Note: I'm aware of a japanese film called \"Laputa: castle in the sky\", where they say that the true Laputa (floating city) is the one appearing on the movie and not the one in the gulliver's travels... I have to disagree, the real one is the described on the Gulliver's travels. the one from the movie has no resemblance, it was not plain below, it didn't had a colossal tree in the middle, althoguh it had geometrical gardens placed around it's central circle going in all directions with very wide streets, with water running on it's both sides to several artificial lakes, but only a few centimeters deep in the streets, while the lakes were very deep. All this of course was white in color, like the buildings that didn't had any right angle, not even triangular or sharp where one might collide with it. And that's what let me survive to the UFO crash, it didn't had any hard or sharp surface, so thanks to that I survived, hopefully one day they'll make the plains, etc like this again. Also I never heard any machine noise in the whole vision, there was only the purest silence, where it was all the harmony at once. Some say that those fallen angels are inside an astral prison in the south pole, behind it's mountains and ice, but not physically, trapped inside a \"simulated\" body, a machine that let's you incarnate in it, but then you can't escape, and the machine never dies, so the machine-body is like a permanent prison, kept almost in absolute darkness. Ages ago there were very dangerous devices capable even to interact with other dimensions, it's better to forget all those things forever, at least the bad ones. Finally did the creators of the Babel tower wanted to arrive to the floating city? Also please watch the cartoon Steven Universe, because tells a very similar story to this, and please notice it's soundtrack vol. 1 picture, it's Pink diamond gem disposed like a floating city between the clouds... for real? Someone here knows about it for sure, and it's curious how the highest knowledge was always encoded as shows or books for kids like the Gulliver novel, Gravity Falls, Steven Universe, Monster High, etc. The best way to hide something really is in plain sight by the looks of it. If you read this far, let me congratulate you, I'm surprised, and I really hope that at least you'll find it like a a science fiction story or perhaps something that might have happened very long ago.\\par\\par\r\n" +
                "\\b - \"Conclusions\":\\b0  we use glass for the lenses of our telescopes, etc, but why this has to be the unique or best material in the universe. Couldn't it be a better material that could have less deviation or loss when magnifying? Why don't try even lenses made of a liquid material like water, but contained somehow? I now remember a techology thousands of years more evolved, although it was from an ancient time, which is even more strange. Of course the easiest way would be say it's all fantasy or nonsense.. But after that vision now I know that death is not the end, but another state of things. The worse thing to do is have fear of whatever thing, that's were we're more vulnerable, so don't fear anything. And try not to think the way most do, try to question anything don't believe that becasuse most people make one thing in a particular way, that is the best ever possible way of doing it. Thanks for reading, remember to get in contact if you're interested to the mail mentioned below and have a wonderful day! :-)\\par\\par\r\n" +
                "\\b - \"Contact\":\\b0  Jupitermauro@gmail.com.\\par\r\n" +
                "\\pard\\sa200\\sl276\\slmult1\\f1\\fs22\\lang10\\par\r\n}";
                RichTextBox_Infiniscopio.Rtf = Texto_Ayuda;
                RichTextBox_Infiniscopio.ZoomFactor = Zoom != 1.5f ? 1.5f : 2.5f;
                RichTextBox_Infiniscopio.ZoomFactor = Zoom;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
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
                //if (this.WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        this.Close();
                    }
                    else if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Cargar_Opciones()
        {
            try
            {
                /*RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Template");

                // bool
                try { Variable_ = bool.Parse((string)Clave.GetValue("Variable_", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_ = true; }

                // int
                try { Variable_ = (int)Clave.GetValue("Variable_", 0); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_ = 0; }
                
                // Correct any bad value after loading:
                if ((int)Variable_ < 0 || (int)Variable_ > (int)Variables.Variable) Variable_ = Variables.Variable;

                // Apply all the loaded values:
                ComboBox_Variable_.SelectedIndex = (int)Variable_;

                Menú_Contextual_Variable_.Checked = Variable_;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Guardar_Opciones()
        {
            try
            {
                /*RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Template");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        Clave.DeleteValue(Matriz_Nombres[Índice]);
                    }
                }
                Matriz_Nombres = null;
                
                // bool
                try { Clave.SetValue("Variable_", Variable_doDaylightCycle.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                // int
                try { Clave.SetValue("Tickspeed", (int)Variable_, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Restablecer_Opciones()
        {
            try
            {
                /*RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Template");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        try { Clave.DeleteValue(Matriz_Nombres[Índice]); }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    Matriz_Nombres = null;
                }
                Clave.Close();
                Clave = null;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Main_window;
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
                Barra_Estado_Botón_Excepción.Visible = false;
                Barra_Estado_Separador_1.Visible = false;
                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                Barra_Estado_Botón_Excepción.Text = "Exceptions: 0";
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
                Program.Crear_Carpetas(Program.Ruta_Minecraft);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Minecraft, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                /*if (Picture.Image != null)
                {
                    Clipboard.SetImage(Picture.Image);
                    SystemSounds.Asterisk.Play();
                }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                /*if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Minecraft);
                    Picture.Image.Save(Program.Ruta_Minecraft + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + ".png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }*/
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

        private void Temporizador_Principal_Tick(object sender, EventArgs e)
        {
            try
            {
                int Tick = Environment.TickCount;
                try
                {
                    if (Variable_Excepción)
                    {
                        if ((Environment.TickCount / 500) % 2 == 0)
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
                try
                {
                    try
                    {
                        if (Tick % 250 == 0) // Only update every quarter second
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
                            if (Memoria_Bytes < 4294967296L) // < 4 GB
                            {
                                if (Variable_Memoria)
                                {
                                    Variable_Memoria = false;
                                    Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Black;
                                }
                            }
                            else // >= 4 GB
                            {
                                if ((Environment.TickCount / 500) % 2 == 0)
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
                try
                {
                    if (Variable_Zoom != RichTextBox_Infiniscopio.ZoomFactor)
                    {
                        Variable_Zoom = RichTextBox_Infiniscopio.ZoomFactor;
                        Registro_Guardar_Opciones();
                        this.Text = Texto_Título + " - [Zoom: " + Program.Traducir_Número(Variable_Zoom) + "x]";
                    }
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                long FPS_Milisegundo = FPS_Cronómetro.ElapsedMilliseconds;
                long FPS_Segundo = FPS_Milisegundo / 1000L;
                if (FPS_Segundo != FPS_Segundo_Anterior)
                {
                    FPS_Segundo_Anterior = FPS_Segundo;
                    FPS_Real = FPS_Temporal;
                    Barra_Estado_Etiqueta_FPS.Text = FPS_Real.ToString() + " FPS";
                    FPS_Temporal = 0L;
                }
                FPS_Temporal++;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Jonathan_Swift_Click(object sender, EventArgs e)
        {
            try
            {
                bool Mostrar = !RichTextBox_Infiniscopio.Visible;
                RichTextBox_Infiniscopio.Visible = Mostrar;
                Tabla_Principal.Visible = !Mostrar;
                if (!Mostrar)
                {
                    Botón_Jonathan_Swift.Image = Resources.Ojo_Ciego;
                    Botón_Jonathan_Swift.Text = " Hide the \"Jonathan Swift Laputa Floating City\" (the houses are a bit different from what I remember) and the \"Steven Universe Soundtrack vol. 1\" pictures... ";
                }
                else
                {
                    Botón_Jonathan_Swift.Image = Resources.Ojo;
                    Botón_Jonathan_Swift.Text = " Show the \"Jonathan Swift Laputa Floating City\" (the houses are a bit different from what I remember) and the \"Steven Universe Soundtrack vol. 1\" pictures... ";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
