using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        public static unsafe ufbx_scene* LoadScene(string pPath, out ufbx_error OutError)
        {
            return In.Internal_LoadScene(pPath, out OutError);
        }

        public static unsafe string UfbxStringToString(ref ufbx_string pString)
        {
            string neo = "";
            neo = new string(pString.data, 0, (int)pString.length);

            return neo;
        }





        // ************************************************ //
        //               Helpers from Ufbx.h                //
        // ************************************************ //
        // Utility functions for reading geometry data for a single index.
        //public static unsafe double ufbx_get_vertex_real(ufbx_vertex_real* v, [NativeTypeName("size_t")] UIntPtr index)
        //{
        //    ufbx_assert((int)index < (int)v->indices.count); // ufbx_assert esta en "check_fbx.c"
        //    return v->values.data[(int)v->indices.data[(int)index]];
        //}
        //public static unsafe ufbx_vec2 ufbx_get_vertex_vec2(const ufbx_vertex_vec2* v, size_t index) { ufbx_assert(index < v->indices.count); return v->values.data[(int32_t)v->indices.data[index]]; }
        //public static unsafe ufbx_vec3 ufbx_get_vertex_vec3(const ufbx_vertex_vec3* v, size_t index) { ufbx_assert(index < v->indices.count); return v->values.data[(int32_t)v->indices.data[index]]; }
        //public static unsafe ufbx_vec4 ufbx_get_vertex_vec4(const ufbx_vertex_vec4* v, size_t index) { ufbx_assert(index < v->indices.count); return v->values.data[(int32_t)v->indices.data[index]]; }


    }




    internal static class In
    {

        internal static unsafe ufbx_scene* Internal_LoadScene(string pPath, out ufbx_error OutError)
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










    internal static class Out
    {

    }


















}




// Sui recuerda presionar "insert" para sustituir los espacios por letras.
// ************************************************ //
//                                                  //
// ************************************************ //