using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static Ufbx.UfbxNative;

namespace Ufbx
{
    /*
    -- Variables:
    //starts: m_ = public, Recommended to use getters and setters
    starts: _ = private, NOT local
    starts: lowercase = private, local
    starts: "p" (lowercase) = local incoming parameter
    starts: uppercase = public
    -- Functions:
    starts: uppercase = public
    starts: lowercase = private
    //starts: __ = public, ONLY for modders, Not recommended
    */






    // ************************************************ //
    //                     CsUfbx                       //
    // ************************************************ //
    public static class CsUfbx
    {

        public static unsafe ufbx_scene* LoadFile(string pPath, out ufbx_error OutError)
        {
            byte[] pPathBytes = Encoding.UTF8.GetBytes(pPath + "\0");
            ufbx_load_opts opts = default;
            ufbx_error error = default;
            ufbx_scene* scene;

            fixed (byte* pPathPtr = pPathBytes)
            {
                ufbx_load_opts* pOpts = &opts;
                ufbx_error* pError = &error;
                scene = ufbx_load_file((sbyte*)pPathPtr, pOpts, pError);
            }

            OutError = error;

            if (error.type != ufbx_error_type.UFBX_ERROR_NONE)
                return null;

            return scene;
        }






    }



















}




// Sui recuerda presionar "insert" para sustituir los espacios por letras.
// ************************************************ //
//                                                  //
// ************************************************ //