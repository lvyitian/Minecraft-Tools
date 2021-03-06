﻿/*******************************************************************************
 * Copyright (C) 2014-2015 Anton Gustafsson
 *
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 ******************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XNA_Extractor.Util;

namespace XNA_Extractor.Extract
{
    public static class FFmpeg
    {
        //========== CONSTANTS ===========
        #region Constants

        /**<summary>The path of the temporary executable.</summary>*/
        private static readonly string TempFFmpeg = Program.Ruta_Aplicación + "\\ffmpeg.exe";

        #endregion
        //========= CONSTRUCTORS =========
        #region Constructors

        /**<summary>Extracts FFmpeg.</summary>*/
        static FFmpeg()
        {
            //EmbeddedResources.Extract(TempFFmpeg, Resources.ffmpeg);
        }

        #endregion
        //========== CONVERTING ==========
        #region Converting

        /**<summary>Converts the specified input file to wave format.</summary>*/
        public static bool Convert(string input, string output)
        {
            try
            {
                //List<string> command = new List<string>();
                string arguments =
                    "-i \"" + Path.GetFullPath(input) + "\" " +
                    "-acodec pcm_s16le " +
                    "-nostdin " +
                    "-ab 128k " +
                    "-map_metadata -1 " +
                    "-y " +
                    "\"" + Path.GetFullPath(output) + "\"";

                Process Proceso = Program.Ejecutar_Ruta_Proceso(TempFFmpeg, arguments, ProcessWindowStyle.Hidden);
                if (Proceso != null) Proceso.WaitForExit();
                return Proceso.ExitCode == 0;
                /*ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = TempFFmpeg;
                start.Arguments = arguments;
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.UseShellExecute = true;
                start.Verb = "open";

                Process process = Process.Start(start);
                process.WaitForExit();
                return (process.ExitCode == 0);*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return false;
        }

        #endregion
    }
}
