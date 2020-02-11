using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    /// <summary>
    /// My custom class for encrypting and decrypting byte arrays in a lossless way as long as you encrypt and decrypt the same number of times using the same parameters (but in reverse order for the decrypting). So you could encrypt 100 times in a row with different parameters, but for decrypting then you should start by the last encrypting parameters you used and go backwards. Since these functions are lossless and 100 % reversible, you should never loss any data as long as you remember the way you encrypted the information.
    /// WARNING: using any part of this code to decrypt any of the "secret" files
    /// on your own will be like if you gave your consent to all of the
    /// requirements needed in order to be able to export the files
    /// using regularly the application, so again... YOU'VE BEEN WARNED!
    /// </summary>
    internal static class Jupisoft_Encrypting_Decrypting
    {
        /// <summary>
        /// Tip: you might also apply any matrix to a 2D image to generate cool and
        /// awesome fractals by applying it to the values of the RGB bytes. The best
        /// graphic to show this would be something like the Photoshop curves, but
        /// it should support an input of the encrypting array itself with 256 values.
        /// Also a second "pass" of the same array will revert it's previous effects
        /// in a 100 % lossless way, making it very useful for simple encryption.
        /// </summary>
        internal static readonly byte[] Matriz_Bytes_Invertir_Base_2 = new byte[256] { 0, 128, 64, 192, 32, 160, 96, 224, 16, 144, 80, 208, 48, 176, 112, 240, 8, 136, 72, 200, 40, 168, 104, 232, 24, 152, 88, 216, 56, 184, 120, 248, 4, 132, 68, 196, 36, 164, 100, 228, 20, 148, 84, 212, 52, 180, 116, 244, 12, 140, 76, 204, 44, 172, 108, 236, 28, 156, 92, 220, 60, 188, 124, 252, 2, 130, 66, 194, 34, 162, 98, 226, 18, 146, 82, 210, 50, 178, 114, 242, 10, 138, 74, 202, 42, 170, 106, 234, 26, 154, 90, 218, 58, 186, 122, 250, 6, 134, 70, 198, 38, 166, 102, 230, 22, 150, 86, 214, 54, 182, 118, 246, 14, 142, 78, 206, 46, 174, 110, 238, 30, 158, 94, 222, 62, 190, 126, 254, 1, 129, 65, 193, 33, 161, 97, 225, 17, 145, 81, 209, 49, 177, 113, 241, 9, 137, 73, 201, 41, 169, 105, 233, 25, 153, 89, 217, 57, 185, 121, 249, 5, 133, 69, 197, 37, 165, 101, 229, 21, 149, 85, 213, 53, 181, 117, 245, 13, 141, 77, 205, 45, 173, 109, 237, 29, 157, 93, 221, 61, 189, 125, 253, 3, 131, 67, 195, 35, 163, 99, 227, 19, 147, 83, 211, 51, 179, 115, 243, 11, 139, 75, 203, 43, 171, 107, 235, 27, 155, 91, 219, 59, 187, 123, 251, 7, 135, 71, 199, 39, 167, 103, 231, 23, 151, 87, 215, 55, 183, 119, 247, 15, 143, 79, 207, 47, 175, 111, 239, 31, 159, 95, 223, 63, 191, 127, 255 };
        /// <summary>
        /// Tip: you might also apply any matrix to a 2D image to generate cool and
        /// awesome fractals by applying it to the values of the RGB bytes. The best
        /// graphic to show this would be something like the Photoshop curves, but
        /// it should support an input of the encrypting array itself with 256 values.
        /// Also a second "pass" of the same array will revert it's previous effects
        /// in a 100 % lossless way, making it very useful for simple encryption.
        /// </summary>
        internal static readonly byte[] Matriz_Bytes_Invertir_Base_4 = new byte[256] { 0, 64, 128, 192, 16, 80, 144, 208, 32, 96, 160, 224, 48, 112, 176, 240, 4, 68, 132, 196, 20, 84, 148, 212, 36, 100, 164, 228, 52, 116, 180, 244, 8, 72, 136, 200, 24, 88, 152, 216, 40, 104, 168, 232, 56, 120, 184, 248, 12, 76, 140, 204, 28, 92, 156, 220, 44, 108, 172, 236, 60, 124, 188, 252, 1, 65, 129, 193, 17, 81, 145, 209, 33, 97, 161, 225, 49, 113, 177, 241, 5, 69, 133, 197, 21, 85, 149, 213, 37, 101, 165, 229, 53, 117, 181, 245, 9, 73, 137, 201, 25, 89, 153, 217, 41, 105, 169, 233, 57, 121, 185, 249, 13, 77, 141, 205, 29, 93, 157, 221, 45, 109, 173, 237, 61, 125, 189, 253, 2, 66, 130, 194, 18, 82, 146, 210, 34, 98, 162, 226, 50, 114, 178, 242, 6, 70, 134, 198, 22, 86, 150, 214, 38, 102, 166, 230, 54, 118, 182, 246, 10, 74, 138, 202, 26, 90, 154, 218, 42, 106, 170, 234, 58, 122, 186, 250, 14, 78, 142, 206, 30, 94, 158, 222, 46, 110, 174, 238, 62, 126, 190, 254, 3, 67, 131, 195, 19, 83, 147, 211, 35, 99, 163, 227, 51, 115, 179, 243, 7, 71, 135, 199, 23, 87, 151, 215, 39, 103, 167, 231, 55, 119, 183, 247, 11, 75, 139, 203, 27, 91, 155, 219, 43, 107, 171, 235, 59, 123, 187, 251, 15, 79, 143, 207, 31, 95, 159, 223, 47, 111, 175, 239, 63, 127, 191, 255 };
        /// <summary>
        /// Tip: you might also apply any matrix to a 2D image to generate cool and
        /// awesome fractals by applying it to the values of the RGB bytes. The best
        /// graphic to show this would be something like the Photoshop curves, but
        /// it should support an input of the encrypting array itself with 256 values.
        /// Also a second "pass" of the same array will revert it's previous effects
        /// in a 100 % lossless way, making it very useful for simple encryption.
        /// </summary>
        internal static readonly byte[] Matriz_Bytes_Invertir_Base_16 = new byte[256] { 0, 16, 32, 48, 64, 80, 96, 112, 128, 144, 160, 176, 192, 208, 224, 240, 1, 17, 33, 49, 65, 81, 97, 113, 129, 145, 161, 177, 193, 209, 225, 241, 2, 18, 34, 50, 66, 82, 98, 114, 130, 146, 162, 178, 194, 210, 226, 242, 3, 19, 35, 51, 67, 83, 99, 115, 131, 147, 163, 179, 195, 211, 227, 243, 4, 20, 36, 52, 68, 84, 100, 116, 132, 148, 164, 180, 196, 212, 228, 244, 5, 21, 37, 53, 69, 85, 101, 117, 133, 149, 165, 181, 197, 213, 229, 245, 6, 22, 38, 54, 70, 86, 102, 118, 134, 150, 166, 182, 198, 214, 230, 246, 7, 23, 39, 55, 71, 87, 103, 119, 135, 151, 167, 183, 199, 215, 231, 247, 8, 24, 40, 56, 72, 88, 104, 120, 136, 152, 168, 184, 200, 216, 232, 248, 9, 25, 41, 57, 73, 89, 105, 121, 137, 153, 169, 185, 201, 217, 233, 249, 10, 26, 42, 58, 74, 90, 106, 122, 138, 154, 170, 186, 202, 218, 234, 250, 11, 27, 43, 59, 75, 91, 107, 123, 139, 155, 171, 187, 203, 219, 235, 251, 12, 28, 44, 60, 76, 92, 108, 124, 140, 156, 172, 188, 204, 220, 236, 252, 13, 29, 45, 61, 77, 93, 109, 125, 141, 157, 173, 189, 205, 221, 237, 253, 14, 30, 46, 62, 78, 94, 110, 126, 142, 158, 174, 190, 206, 222, 238, 254, 15, 31, 47, 63, 79, 95, 111, 127, 143, 159, 175, 191, 207, 223, 239, 255 };
        /// <summary>
        /// Tip: you might also apply any matrix to a 2D image to generate cool and
        /// awesome fractals by applying it to the values of the RGB bytes. The best
        /// graphic to show this would be something like the Photoshop curves, but
        /// it should support an input of the encrypting array itself with 256 values.
        /// Also a second "pass" of the same array will revert it's previous effects
        /// in a 100 % lossless way, making it very useful for simple encryption.
        /// </summary>
        internal static readonly byte[] Matriz_Bytes_Negativizar = new byte[256] { 255, 254, 253, 252, 251, 250, 249, 248, 247, 246, 245, 244, 243, 242, 241, 240, 239, 238, 237, 236, 235, 234, 233, 232, 231, 230, 229, 228, 227, 226, 225, 224, 223, 222, 221, 220, 219, 218, 217, 216, 215, 214, 213, 212, 211, 210, 209, 208, 207, 206, 205, 204, 203, 202, 201, 200, 199, 198, 197, 196, 195, 194, 193, 192, 191, 190, 189, 188, 187, 186, 185, 184, 183, 182, 181, 180, 179, 178, 177, 176, 175, 174, 173, 172, 171, 170, 169, 168, 167, 166, 165, 164, 163, 162, 161, 160, 159, 158, 157, 156, 155, 154, 153, 152, 151, 150, 149, 148, 147, 146, 145, 144, 143, 142, 141, 140, 139, 138, 137, 136, 135, 134, 133, 132, 131, 130, 129, 128, 127, 126, 125, 124, 123, 122, 121, 120, 119, 118, 117, 116, 115, 114, 113, 112, 111, 110, 109, 108, 107, 106, 105, 104, 103, 102, 101, 100, 99, 98, 97, 96, 95, 94, 93, 92, 91, 90, 89, 88, 87, 86, 85, 84, 83, 82, 81, 80, 79, 78, 77, 76, 75, 74, 73, 72, 71, 70, 69, 68, 67, 66, 65, 64, 63, 62, 61, 60, 59, 58, 57, 56, 55, 54, 53, 52, 51, 50, 49, 48, 47, 46, 45, 44, 43, 42, 41, 40, 39, 38, 37, 36, 35, 34, 33, 32, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };

        /// <summary>
        /// Made by combining the "Matriz_Bytes_Invertir_Base_4" and "Matriz_Bytes_Invertir_Base_16" byte arrays.
        /// </summary>
        internal static byte[] Matriz_Bytes_Fractal_Rombo = new byte[256] { 0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15, 64, 68, 72, 76, 65, 69, 73, 77, 66, 70, 74, 78, 67, 71, 75, 79, 128, 132, 136, 140, 129, 133, 137, 141, 130, 134, 138, 142, 131, 135, 139, 143, 192, 196, 200, 204, 193, 197, 201, 205, 194, 198, 202, 206, 195, 199, 203, 207, 16, 20, 24, 28, 17, 21, 25, 29, 18, 22, 26, 30, 19, 23, 27, 31, 80, 84, 88, 92, 81, 85, 89, 93, 82, 86, 90, 94, 83, 87, 91, 95, 144, 148, 152, 156, 145, 149, 153, 157, 146, 150, 154, 158, 147, 151, 155, 159, 208, 212, 216, 220, 209, 213, 217, 221, 210, 214, 218, 222, 211, 215, 219, 223, 32, 36, 40, 44, 33, 37, 41, 45, 34, 38, 42, 46, 35, 39, 43, 47, 96, 100, 104, 108, 97, 101, 105, 109, 98, 102, 106, 110, 99, 103, 107, 111, 160, 164, 168, 172, 161, 165, 169, 173, 162, 166, 170, 174, 163, 167, 171, 175, 224, 228, 232, 236, 225, 229, 233, 237, 226, 230, 234, 238, 227, 231, 235, 239, 48, 52, 56, 60, 49, 53, 57, 61, 50, 54, 58, 62, 51, 55, 59, 63, 112, 116, 120, 124, 113, 117, 121, 125, 114, 118, 122, 126, 115, 119, 123, 127, 176, 180, 184, 188, 177, 181, 185, 189, 178, 182, 186, 190, 179, 183, 187, 191, 240, 244, 248, 252, 241, 245, 249, 253, 242, 246, 250, 254, 243, 247, 251, 255 };
        /// <summary>
        /// Made by combining the "Matriz_Bytes_Invertir_Base_2" and "Matriz_Bytes_Invertir_Base_4" byte arrays.
        /// </summary>
        internal static byte[] Matriz_Bytes_Fractal_Rombo_Simple = new byte[256] { 0, 2, 1, 3, 8, 10, 9, 11, 4, 6, 5, 7, 12, 14, 13, 15, 32, 34, 33, 35, 40, 42, 41, 43, 36, 38, 37, 39, 44, 46, 45, 47, 16, 18, 17, 19, 24, 26, 25, 27, 20, 22, 21, 23, 28, 30, 29, 31, 48, 50, 49, 51, 56, 58, 57, 59, 52, 54, 53, 55, 60, 62, 61, 63, 128, 130, 129, 131, 136, 138, 137, 139, 132, 134, 133, 135, 140, 142, 141, 143, 160, 162, 161, 163, 168, 170, 169, 171, 164, 166, 165, 167, 172, 174, 173, 175, 144, 146, 145, 147, 152, 154, 153, 155, 148, 150, 149, 151, 156, 158, 157, 159, 176, 178, 177, 179, 184, 186, 185, 187, 180, 182, 181, 183, 188, 190, 189, 191, 64, 66, 65, 67, 72, 74, 73, 75, 68, 70, 69, 71, 76, 78, 77, 79, 96, 98, 97, 99, 104, 106, 105, 107, 100, 102, 101, 103, 108, 110, 109, 111, 80, 82, 81, 83, 88, 90, 89, 91, 84, 86, 85, 87, 92, 94, 93, 95, 112, 114, 113, 115, 120, 122, 121, 123, 116, 118, 117, 119, 124, 126, 125, 127, 192, 194, 193, 195, 200, 202, 201, 203, 196, 198, 197, 199, 204, 206, 205, 207, 224, 226, 225, 227, 232, 234, 233, 235, 228, 230, 229, 231, 236, 238, 237, 239, 208, 210, 209, 211, 216, 218, 217, 219, 212, 214, 213, 215, 220, 222, 221, 223, 240, 242, 241, 243, 248, 250, 249, 251, 244, 246, 245, 247, 252, 254, 253, 255 };
        /// <summary>
        /// Made by combining the "Matriz_Bytes_Invertir_Base_2" and "Matriz_Bytes_Invertir_Base_16" byte arrays.
        /// </summary>
        internal static byte[] Matriz_Bytes_Fractal_Círculo = new byte[256] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15, 128, 136, 132, 140, 130, 138, 134, 142, 129, 137, 133, 141, 131, 139, 135, 143, 64, 72, 68, 76, 66, 74, 70, 78, 65, 73, 69, 77, 67, 75, 71, 79, 192, 200, 196, 204, 194, 202, 198, 206, 193, 201, 197, 205, 195, 203, 199, 207, 32, 40, 36, 44, 34, 42, 38, 46, 33, 41, 37, 45, 35, 43, 39, 47, 160, 168, 164, 172, 162, 170, 166, 174, 161, 169, 165, 173, 163, 171, 167, 175, 96, 104, 100, 108, 98, 106, 102, 110, 97, 105, 101, 109, 99, 107, 103, 111, 224, 232, 228, 236, 226, 234, 230, 238, 225, 233, 229, 237, 227, 235, 231, 239, 16, 24, 20, 28, 18, 26, 22, 30, 17, 25, 21, 29, 19, 27, 23, 31, 144, 152, 148, 156, 146, 154, 150, 158, 145, 153, 149, 157, 147, 155, 151, 159, 80, 88, 84, 92, 82, 90, 86, 94, 81, 89, 85, 93, 83, 91, 87, 95, 208, 216, 212, 220, 210, 218, 214, 222, 209, 217, 213, 221, 211, 219, 215, 223, 48, 56, 52, 60, 50, 58, 54, 62, 49, 57, 53, 61, 51, 59, 55, 63, 176, 184, 180, 188, 178, 186, 182, 190, 177, 185, 181, 189, 179, 187, 183, 191, 112, 120, 116, 124, 114, 122, 118, 126, 113, 121, 117, 125, 115, 123, 119, 127, 240, 248, 244, 252, 242, 250, 246, 254, 241, 249, 245, 253, 243, 251, 247, 255 };
        /// <summary>
        /// Made by combining the "Matriz_Bytes_Invertir_Base_2", "Matriz_Bytes_Invertir_Base_4" and "Matriz_Bytes_Invertir_Base_16" byte arrays.
        /// </summary>
        internal static byte[] Matriz_Bytes_Fractal_2_4_16 = new byte[256] { 0, 32, 16, 48, 128, 160, 144, 176, 64, 96, 80, 112, 192, 224, 208, 240, 2, 34, 18, 50, 130, 162, 146, 178, 66, 98, 82, 114, 194, 226, 210, 242, 1, 33, 17, 49, 129, 161, 145, 177, 65, 97, 81, 113, 193, 225, 209, 241, 3, 35, 19, 51, 131, 163, 147, 179, 67, 99, 83, 115, 195, 227, 211, 243, 8, 40, 24, 56, 136, 168, 152, 184, 72, 104, 88, 120, 200, 232, 216, 248, 10, 42, 26, 58, 138, 170, 154, 186, 74, 106, 90, 122, 202, 234, 218, 250, 9, 41, 25, 57, 137, 169, 153, 185, 73, 105, 89, 121, 201, 233, 217, 249, 11, 43, 27, 59, 139, 171, 155, 187, 75, 107, 91, 123, 203, 235, 219, 251, 4, 36, 20, 52, 132, 164, 148, 180, 68, 100, 84, 116, 196, 228, 212, 244, 6, 38, 22, 54, 134, 166, 150, 182, 70, 102, 86, 118, 198, 230, 214, 246, 5, 37, 21, 53, 133, 165, 149, 181, 69, 101, 85, 117, 197, 229, 213, 245, 7, 39, 23, 55, 135, 167, 151, 183, 71, 103, 87, 119, 199, 231, 215, 247, 12, 44, 28, 60, 140, 172, 156, 188, 76, 108, 92, 124, 204, 236, 220, 252, 14, 46, 30, 62, 142, 174, 158, 190, 78, 110, 94, 126, 206, 238, 222, 254, 13, 45, 29, 61, 141, 173, 157, 189, 77, 109, 93, 125, 205, 237, 221, 253, 15, 47, 31, 63, 143, 175, 159, 191, 79, 111, 95, 127, 207, 239, 223, 255 };

        /// <summary>
        /// Encrypts a byte array using the selected parameters.
        /// </summary>
        /// <param name="Matriz_Bytes">Any valid byte array.</param>
        /// <param name="Invertir_Base_2">If it's true the byte array bits will be inverted in base 2.</param>
        /// <param name="Invertir_Base_4">If it's true the byte array bits will be inverted in base 4.</param>
        /// <param name="Invertir_Base_16">If it's true the byte array bits will be inverted in base 16.</param>
        /// <param name="Negativizar">If it's true the byte array bits will be inverted (0 will be 1 and 1 will be 0).</param>
        /// <returns>Returns the encrypted byte array. Returns null on any error.</returns>
        internal static byte[] Encriptar_Matriz_Bytes(byte[] Matriz_Bytes, bool Invertir_Base_2, bool Invertir_Base_4, bool Invertir_Base_16, bool Negativizar)
        {
            try
            {
                if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Bytes.Length; Índice++)
                    {
                        // Use the regular order for decrypting the byte array.
                        // Note that the encrypting or decrypting order matters.
                        // These funtions are 100 % reversible on a second call.
                        if (Invertir_Base_2) Matriz_Bytes[Índice] = Matriz_Bytes_Invertir_Base_2[Matriz_Bytes[Índice]];
                        if (Invertir_Base_4) Matriz_Bytes[Índice] = Matriz_Bytes_Invertir_Base_4[Matriz_Bytes[Índice]];
                        if (Invertir_Base_16) Matriz_Bytes[Índice] = Matriz_Bytes_Invertir_Base_16[Matriz_Bytes[Índice]];
                        if (Negativizar) Matriz_Bytes[Índice] = Matriz_Bytes_Negativizar[Matriz_Bytes[Índice]];
                    }
                    return Matriz_Bytes;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Decrypts a byte array using the selected parameters.
        /// </summary>
        /// <param name="Matriz_Bytes">Any valid byte array.</param>
        /// <param name="Invertir_Base_2">If it's true the byte array bits will be inverted in base 2.</param>
        /// <param name="Invertir_Base_4">If it's true the byte array bits will be inverted in base 4.</param>
        /// <param name="Invertir_Base_16">If it's true the byte array bits will be inverted in base 16.</param>
        /// <param name="Negativizar">If it's true the byte array bits will be inverted (0 will be 1 and 1 will be 0).</param>
        /// <returns>Returns the decrypted byte array. Returns null on any error.</returns>
        internal static byte[] Desencriptar_Matriz_Bytes(byte[] Matriz_Bytes, bool Invertir_Base_2, bool Invertir_Base_4, bool Invertir_Base_16, bool Negativizar)
        {
            try
            {
                if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Bytes.Length; Índice++)
                    {
                        // Use an inverted order for decrypting the byte array.
                        // Note that the encrypting or decrypting order matters.
                        // These funtions are 100 % reversible on a second call.
                        if (Negativizar) Matriz_Bytes[Índice] = Matriz_Bytes_Negativizar[Matriz_Bytes[Índice]];
                        if (Invertir_Base_16) Matriz_Bytes[Índice] = Matriz_Bytes_Invertir_Base_16[Matriz_Bytes[Índice]];
                        if (Invertir_Base_4) Matriz_Bytes[Índice] = Matriz_Bytes_Invertir_Base_4[Matriz_Bytes[Índice]];
                        if (Invertir_Base_2) Matriz_Bytes[Índice] = Matriz_Bytes_Invertir_Base_2[Matriz_Bytes[Índice]];
                    }
                    return Matriz_Bytes;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Encrypts or decrypts a byte array using a fractal circle pattern (only seen after combining the used encrypting byte arrays and show them in a 2D grid like the curves tool in Photoshop).
        /// </summary>
        /// <param name="Matriz_Bytes">Any valid byte array.</param>
        /// <param name="Desencriptar">If it's true the byte array will be decrypted. If it's false will be encrypted.</param>
        /// <returns>Returns the modified byte array. Returns null on any error.</returns>
        internal static byte[] Encriptar_Desencriptar_Matriz_Bytes_Fractal_Círculo(byte[] Matriz_Bytes, bool Desencriptar)
        {
            try
            {
                if (!Desencriptar) return Encriptar_Matriz_Bytes(Matriz_Bytes, true, false, true, false);
                else return Desencriptar_Matriz_Bytes(Matriz_Bytes, true, false, true, false);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Encrypts or decrypts a byte array using a fractal full rhombus pattern (only seen after combining the used encrypting byte arrays and show them in a 2D grid like the curves tool in Photoshop).
        /// </summary>
        /// <param name="Matriz_Bytes">Any valid byte array.</param>
        /// <param name="Desencriptar">If it's true the byte array will be decrypted. If it's false will be encrypted.</param>
        /// <returns>Returns the modified byte array. Returns null on any error.</returns>
        internal static byte[] Encriptar_Desencriptar_Matriz_Bytes_Fractal_Rombo(byte[] Matriz_Bytes, bool Desencriptar)
        {
            try
            {
                if (!Desencriptar) return Encriptar_Matriz_Bytes(Matriz_Bytes, true, false, true, false);
                else return Desencriptar_Matriz_Bytes(Matriz_Bytes, true, false, true, false);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Encrypts or decrypts a byte array using a fractal simple rhombus pattern (only seen after combining the used encrypting byte arrays and show them in a 2D grid like the curves tool in Photoshop).
        /// </summary>
        /// <param name="Matriz_Bytes">Any valid byte array.</param>
        /// <param name="Desencriptar">If it's true the byte array will be decrypted. If it's false will be encrypted.</param>
        /// <returns>Returns the modified byte array. Returns null on any error.</returns>
        internal static byte[] Encriptar_Desencriptar_Matriz_Bytes_Fractal_Rombo_Simple(byte[] Matriz_Bytes, bool Desencriptar)
        {
            try
            {
                if (!Desencriptar) return Encriptar_Matriz_Bytes(Matriz_Bytes, true, true, false, false);
                else return Desencriptar_Matriz_Bytes(Matriz_Bytes, true, true, false, false);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }
    }
}
