using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FBXRuntime
{
	// -- Mis Functions
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
	// Native ufbx is 1:1


    /*










    // ************************************************ //
    //                       UFBX                       //
    // ************************************************ //
    public static class Ufbx
    {

    }






























    // ************************************************ //
    //                    STRUCTURES                    //
    // ************************************************ //
    // --- 1. Vectores y Tipos Básicos ---
    [StructLayout(LayoutKind.Sequential)]
    public struct Vec3
    {
        public float x, y, z;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Vec2
    {
        public float x, y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ElementList
    {
        public void* data;
        public UIntPtr count;
    }

    // --- 2. Estructuras Principales ---
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Mesh
    {
        public ElementList submeshes;
        public UIntPtr num_vertices;
        public Vec3* vertex_position;
        public Vec3* vertex_normal;
        public Vec2* vertex_uv;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Scene
    {
        public ElementList nodes;
        public ElementList meshes;
    }





    // ************************************************ //
    //             STRUCTURES NATIVES 1:1               //
    // ************************************************ //

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_atomic_counter
    {
        // alignas(std::atomic_size_t) char data[sizeof(std::atomic_size_t)];
        public UIntPtr data;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_bigint
    {
        public void* limbs; // Original: ufbxi_bigint_limb *
        public uint capacity;
        public uint length;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_bit_stream
    {
        public UIntPtr input_left;
        public void* read_fn; // Original: ufbx_read_fn *
        public void* read_user;
        public byte* buffer;
        public UIntPtr buffer_size;
        public byte* chunk_begin;
        public byte* chunk_ptr;
        public byte* chunk_yield;
        public byte* chunk_end;
        public byte* chunk_real_end;
        public UIntPtr num_read_before_chunk;
        public ulong progress_bias;
        public ulong progress_total;
        public UIntPtr progress_interval;
        public ulong bits;
        public UIntPtr left;
        // ufbx_progress_cb suele ser una estructura con un puntero a función
        public ufbx_progress_cb progress_cb;
        public ulong cancel_bits;
        public bool cancelled;
        public fixed byte local_buffer[256];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_huff_tree
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UFBXI_HUFF_FAST_SIZE)]
        public ufbxi_huff_sym[] fast_sym;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UFBXI_HUFF_MAX_LONG_SYMS)]
        public ufbxi_huff_sym[] long_sym;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UFBXI_HUFF_MAX_VALUE)]
        public ufbxi_huff_sym[] sorted_to_sym;

        public fixed uint extra_shift_base[UFBXI_HUFF_MAX_EXTRA_SYMS];
        public fixed ushort extra_mask[UFBXI_HUFF_MAX_EXTRA_SYMS];
        public fixed ushort past_max_code[UFBXI_HUFF_MAX_BITS];
        public fixed short code_to_sorted[UFBXI_HUFF_MAX_BITS];

        public uint num_symbols;
        public uint end_of_block_bits;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_trees
    {
        // Desenroscado de la unión "trees[2]" vs "lit_length / dist"
        public ufbxi_huff_tree lit_length;
        public ufbxi_huff_tree dist;
        public uint fast_bits;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_inflate_retain_imp
    {
        public bool initialized;
        public ufbxi_trees static_trees;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_deflate_context
    {
        public ufbxi_bit_stream stream;
        public uint fast_bits;
        public byte* out_begin;
        public byte* out_ptr;
        public byte* out_end;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_print_buffer
    {
        public byte* dst;
        public UIntPtr length;
        public UIntPtr pos;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_allocator
    {
        public void* error; // Original: ufbx_error *
        public UIntPtr current_size;
        public UIntPtr max_size;
        public UIntPtr num_allocs;
        public UIntPtr max_allocs;
        public UIntPtr huge_size;
        public UIntPtr chunk_max;
        public ufbx_allocator_opts ator;
        public byte* name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_buf
    {
        public ufbxi_allocator* ator;
        public ufbxi_buf_chunk* chunk_0; // Desenroscado de chunks[2]
        public ufbxi_buf_chunk* chunk_1;
        public UIntPtr pos;
        public UIntPtr size;
        public UIntPtr num_items;
        public UIntPtr pushed_size;
        public bool unordered;
        public bool clearable;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_buf_state
    {
        public ufbxi_buf_chunk* chunk;
        public UIntPtr pos;
        public UIntPtr num_items;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_map
    {
        public ufbxi_allocator* ator;
        public UIntPtr data_size;
        public void* items;
        public ulong* entries;
        public uint mask;
        public uint capacity;
        public uint size;
        public void* cmp_fn; // Original: ufbxi_cmp_fn *
        public void* cmp_user;
        public ufbxi_buf aa_buf;
        public ufbxi_aa_node* aa_root;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_warnings
    {
        public void* error; // Original: ufbx_error *
        public ufbxi_buf* result;
        public ufbxi_buf tmp_stack;
        public uint deferred_element_id_plus_one;

        // Original: ufbx_warning *prev_warnings[UFBX_WARNING_TYPE_COUNT][2]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UFBX_WARNING_TYPE_COUNT * 2)]
        public IntPtr[] prev_warnings;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_string_pool
    {
        public void* error;
        public ufbxi_buf buf;
        public ufbxi_map map;
        public UIntPtr initial_size;
        public byte* temp_str;
        public UIntPtr temp_cap;
        public int error_handling; // Suponiendo ufbx_unicode_error_handling como enum int
        public ufbxi_warnings* warnings;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_sanitized_string
    {
        public byte* raw_data;
        public uint raw_length;
        public uint utf8_length;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_task_imp
    {
        public ufbxi_task task;
        public void* fn; // Original: ufbxi_task_fn *
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_task_group
    {
        public uint max_index;
        public uint wait_index;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_value_array
    {
        public void* data;
        public UIntPtr size;
        public byte type;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_scene_imp
    {
        public ufbxi_refcount refcount;
        public ufbx_scene scene;
        public uint magic;
        public ufbxi_buf string_buf;
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct ufbxi_ascii_token_value
    {
        [FieldOffset(0)] public double f64;
        [FieldOffset(0)] public long i64;
        [FieldOffset(0)] public UIntPtr name_len;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_ascii_token
    {
        public byte* str_data;
        public UIntPtr str_len;
        public UIntPtr str_cap;
        public byte type;
        public bool negative;
        public ufbxi_ascii_token_value value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_ascii
    {
        public UIntPtr max_token_length;
        public byte* src;
        public byte* src_yield;
        public byte* src_end;
        public bool read_first_comment;
        public bool found_version;
        public bool parse_as_f32;
        public bool src_is_retained;
        public ufbxi_buf* retain_buf;
        public ufbxi_buf* src_buf;
        public ufbxi_ascii_token prev_token;
        public ufbxi_ascii_token token;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_template
    {
        public byte* type;
        public string sub_type;
        public ufbx_props props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_fbx_id_entry
    {
        public ulong fbx_id;
        public uint element_id;
        public uint user_id;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_ptr_fbx_id_entry
    {
        public ufbxi_ptr_id ptr_id;
        public ulong fbx_id;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_fbx_attr_entry
    {
        public ulong node_fbx_id;
        public ulong attr_fbx_id;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_tmp_connection
    {
        public ulong src, dst;
        public string src_prop;
        public string dst_prop;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_element_info
    {
        public ulong fbx_id;
        public string name;
        public ufbx_props props;
        public void* dom_node; // Original: ufbx_dom_node *
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_tmp_bone_pose
    {
        public ulong bone_fbx_id;
        public ufbx_matrix bone_to_world;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_tmp_mesh_texture
    {
        public string prop_name;
        public uint* face_texture;
        public UIntPtr num_faces;
        public bool all_same;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_mesh_extra
    {
        public ufbxi_tmp_mesh_texture* texture_arr;
        public UIntPtr texture_count;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_tmp_material_texture
    {
        public int material_id;
        public int texture_id;
        public string prop_name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_texture_extra
    {
        public int* blend_modes;
        public UIntPtr num_blend_modes;
        public double* alphas; // Suponiendo ufbx_real = double
        public UIntPtr num_alphas;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_obj_index_range
    {
        public ulong min_ix, max_ix;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_obj_mesh
    {
        public UIntPtr num_faces;
        public UIntPtr num_indices;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UFBXI_OBJ_NUM_ATTRIBS)]
        public ufbxi_obj_index_range[] vertex_range;

        public void* fbx_node; // Original: ufbx_node *
        public void* fbx_mesh; // Original: ufbx_mesh *
        public ulong fbx_node_id;
        public ulong fbx_mesh_id;
        public uint usemtl_base;
        public uint num_groups;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_obj_group_entry
    {
        public byte* name;
        public uint local_id;
        public uint mesh_id;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_obj_fast_indices
    {
        public ulong* indices;
        public UIntPtr num_left;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_tmp_anim_stack
    {
        public byte* name;
        public void* stack; // Original: ufbx_anim_stack *
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_file_content
    {
        public string absolute_filename;
        public ufbx_blob content;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_obj_context
    {
        public string line;
        public string* tokens;
        public UIntPtr tokens_cap;
        public UIntPtr num_tokens;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UFBXI_OBJ_NUM_ATTRIBS)]
        public ufbxi_obj_fast_indices[] fast_indices;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UFBXI_OBJ_NUM_ATTRIBS_EXT)]
        public UIntPtr[] vertex_count;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UFBXI_OBJ_NUM_ATTRIBS_EXT)]
        public ufbxi_buf[] tmp_vertices;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UFBXI_OBJ_NUM_ATTRIBS_EXT)]
        public ufbxi_buf[] tmp_indices;

        public ufbxi_buf tmp_color_valid;
        public ufbxi_buf tmp_faces;
        public ufbxi_buf tmp_face_smoothing;
        public ufbxi_buf tmp_face_group;
        public ufbxi_buf tmp_face_group_infos;
        public ufbxi_buf tmp_face_material;
        public ufbxi_buf tmp_meshes;
        public ufbxi_buf tmp_props;

        public ufbxi_map group_map;
        public UIntPtr read_progress;
        public ufbxi_obj_mesh* mesh;

        public ulong usemtl_fbx_id;
        public uint usemtl_index;
        public uint face_material;
        public uint face_group;
        public bool has_face_group;
        public bool face_smoothing;
        public bool has_face_smoothing;
        public bool has_vertex_color;
        public UIntPtr mrgb_vertex_count;
        public bool eof;
        public bool initialized;
        public ufbx_blob mtllib_relative_path;

        public void** tmp_materials; // Original: ufbx_material **
        public UIntPtr tmp_materials_cap;

        public string object_name; // 'object' es reservado en C#, usar 'object_name'
        public string group;
        public bool material_dirty;
        public bool object_dirty;
        public bool group_dirty;
        public bool face_group_dirty;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_context
    {
        public void* error; // Original: ufbx_error
        public uint version;
        public int exporter; // Suponiendo ufbx_exporter = enum
        public uint exporter_version;
        public bool from_ascii;
        public bool local_big_endian;
        public bool file_big_endian;
        public bool sure_fbx;
        public bool retain_mesh_parts;
        public bool read_legacy_settings;
        public uint double_parse_flags;

        public ufbx_load_opts opts;

        public ulong data_offset;
        public void* read_fn; // Original: ufbx_read_fn *
        public void* skip_fn; // Original: ufbx_skip_fn *
        public void* read_user;
        public byte* read_buffer;
        public UIntPtr read_buffer_size;
        public byte* data_begin;
        public byte* data;
        public UIntPtr yield_size;
        public UIntPtr data_size;

        public ufbxi_allocator ator_result;
        public ufbxi_allocator ator_tmp;

        public ufbxi_map prop_type_map;
        public ufbxi_map fbx_id_map;
        public ufbxi_map ptr_fbx_id_map;
        public ufbxi_map texture_file_map;
        public ufbxi_map anim_stack_map;
        public ufbxi_map fbx_attr_map;
        public ufbxi_map node_prop_set;
        public ufbxi_map dom_node_map;

        public byte* tmp_arr;
        public UIntPtr tmp_arr_size;
        public byte* swap_arr;
        public UIntPtr swap_arr_size;

        public UIntPtr max_zero_indices;
        public UIntPtr max_consecutive_indices;

        public ufbxi_buf tmp;
        public ufbxi_buf tmp_parse;
        public ufbxi_buf tmp_stack;
        public ufbxi_buf tmp_connections;
        public ufbxi_buf tmp_node_ids;
        public ufbxi_buf tmp_elements;
        public ufbxi_buf tmp_element_offsets;
        public ufbxi_buf tmp_element_fbx_ids;
        public ufbxi_buf tmp_element_ptrs;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UFBX_ELEMENT_TYPE_COUNT)]
        public ufbxi_buf[] tmp_typed_element_offsets;

        public ufbxi_buf tmp_mesh_textures;
        public ufbxi_buf tmp_full_weights;
        public ufbxi_buf tmp_dom_nodes;
        public ufbxi_buf tmp_element_id;
        public ufbxi_buf tmp_ascii_spans;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UFBX_THREAD_GROUP_COUNT)]
        public ufbxi_buf[] tmp_thread_parse;

        public UIntPtr tmp_element_byte_offset;
        public ufbxi_template* templates;
        public UIntPtr num_templates;

        public void* dom_parse_toplevel; // Original: ufbx_dom_node *
        public UIntPtr dom_parse_num_children;
        public uint* p_element_id;
        public ufbxi_string_pool string_pool;
        public ufbxi_buf result;

        public ufbxi_node* top_nodes;
        public UIntPtr top_nodes_len, top_nodes_cap;
        public bool parsed_to_end;

        public ufbxi_node* top_node;
        public UIntPtr top_child_index;
        public ufbxi_node top_child; // AVISO: Struct se define mas abajo, cuidado en interop de structs por valor
        public bool has_next_child;

        public uint* zero_indices;
        public uint* consecutive_indices;

        public UIntPtr progress_timer; // ptrdiff_t
        public ulong progress_bytes_total;
        public ulong progress_bytes_latest; // original: latest_progress_bytes
        public UIntPtr progress_interval;

        public void** element_extra_arr;
        public UIntPtr element_extra_cap;
        public byte* tmp_element_flag;

        public void* close_fn; // Original: ufbx_close_fn *
        public void* size_fn;  // Original: ufbx_size_fn *

        public ufbxi_ascii ascii;
        public ulong synthetic_id_counter;

        public bool has_geometry_transform_nodes;
        public bool has_scale_helper_nodes;
        public bool retain_vertex_w;
        public bool blender_full_weights;

        public int mirror_axis; // ufbx_mirror_axis

        public ufbxi_node root; // Struct anidado

        public ufbx_scene scene;
        public ufbxi_scene_imp* scene_imp;
        public void* inflate_retain; // ufbx_inflate_retain *

        public uint* tmp_mesh_consecutive_indices;
        public ulong root_id;
        public uint num_elements;

        public ufbxi_node legacy_node;
        public ulong legacy_implicit_anim_layer_id;

        public ufbxi_file_content* file_content;
        public UIntPtr num_file_content;

        public long ktime_sec;
        public double ktime_sec_double;
        public bool eof;

        public ufbxi_obj_context obj;

        public ufbx_matrix axis_matrix;
        public double unit_scale; // Suponiendo ufbx_real

        public ufbxi_warnings warnings;
        public bool deferred_failure;
        public bool deferred_load;

        public byte* load_filename;
        public UIntPtr load_filename_len;

        public bool parse_threaded;
        public ufbxi_thread_pool thread_pool;
        public byte* base64_table;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_file_context
    {
        public void* error; // Original: ufbx_error
        public ufbxi_allocator* parent_ator;
        public ufbxi_allocator ator;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_memory_stream
    {
        public void* data;
        public UIntPtr size;
        public UIntPtr position;
        public void* close_cb; // Original: ufbx_close_memory_cb
        public UIntPtr self_size;
        public ufbxi_allocator* parent_ator;
        public ufbxi_allocator local_ator;
        public void* error; // ufbx_error
        public byte data_copy_0; // Array flexible (char data_copy[])
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_xml_context
    {
        public void* error;
        public ufbxi_allocator* ator;
        public ufbxi_buf tmp_stack;
        public ufbxi_buf result;
        public ufbxi_xml_document* doc;
        public void* read_fn; // ufbx_read_fn *
        public void* read_user;
        public byte* tok;
        public UIntPtr tok_cap;
        public UIntPtr tok_len;
        public byte* pos;
        public byte* pos_end;
        public fixed byte data[4096];
        public bool io_error;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_xml_load_opts
    {
        public ufbxi_allocator* ator;
        public void* read_fn;
        public void* read_user;
        public byte* prefix;
        public UIntPtr prefix_length;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_array_info
    {
        public byte type;
        public byte flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_deflate_task
    {
        public UIntPtr encoded_size;
        public UIntPtr src_elem_size;
        public UIntPtr array_size;
        public byte src_type;
        public byte dst_type;
        public byte arr_type;
        public void* encoded_data;
        public void* decoded_data;
        public void* dst_data;
        public void* inflate_retain; // ufbx_inflate_retain *
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_ascii_span
    {
        public byte* source;
        public UIntPtr length;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_ascii_array_task
    {
        public void* arr_data;
        public byte arr_type;
        public UIntPtr arr_size;
        public ufbxi_ascii_span* spans;
        public UIntPtr num_spans;
        public UIntPtr offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_dom_mapping
    {
        public UIntPtr node_ptr;
        public void* dom_node; // ufbx_dom_node *
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_prop_type_name
    {
        public byte* name;
        public int type; // Suponiendo ufbx_prop_type = enum
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_texture_file_entry
    {
        public byte* key;
        public void* file; // ufbx_texture_file *
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_node_extra
    {
        public uint geometry_helper_id;
        public uint scale_helper_id;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_scale_helper_prop
    {
        public byte* name;
        public ufbx_vec3 default_value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_tangent_layer
    {
        public ufbx_vertex_vec3 elem;
        public uint index;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_id_group
    {
        public uint id;
        public uint index;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_constraint_type
    {
        public int type; // ufbx_constraint_type
        public byte* name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_object_batch
    {
        public ufbxi_node** nodes;
        public UIntPtr num_nodes;
        public uint task_index;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_legacy_prop
    {
        public byte* prop_name;
        public int prop_type; // ufbx_prop_type
        public byte* node_name;
        public byte* node_fmt;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_pre_connection
    {
        public void* src; // ufbx_element *
        public void* dst;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_pre_node
    {
        public bool has_constant_scale;
        public bool has_recursive_scale_helper;
        public bool has_skin_deformer;
        public ufbx_vec3 constant_scale;
        public uint element_id;
        public uint first_child;
        public uint next_child;
        public uint parent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_pre_mesh
    {
        public bool has_skin_deformer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_pre_anim_value
    {
        public bool has_constant_value;
        public ufbx_vec3 constant_value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_shader_mapping
    {
        public byte index;
        public byte flags;
        public byte transform;
        public byte prop_len;
        public byte* prop;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_shader_mapping_list
    {
        public ufbxi_shader_mapping* data;
        public UIntPtr count;
        public ufbxi_shader_mapping* features;
        public UIntPtr feature_count;
        public uint default_features;
        public string texture_prefix;
        public string texture_suffix;
        public string texture_enabled_prefix;
        public string texture_enabled_suffix;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_glossiness_remap
    {
        public byte feature;
        public byte roughness_map;
        public byte glossiness_map;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_constraint_prop
    {
        public int type; // ufbxi_constraint_prop_type
        public byte* name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_file_shader
    {
        public ulong shader_id;
        public byte* shader_name;
        public byte* input_name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_ordered_texture
    {
        public void* texture; // ufbx_texture *
        public UIntPtr order;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_anim_imp
    {
        public ufbxi_refcount refcount; // Definido más abajo
        public ufbx_anim anim;
        public uint magic;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_aperture_format
    {
        public ushort film_size_x;
        public ushort film_size_y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_geometry_cache_imp
    {
        public ufbxi_refcount refcount;
        public ufbx_geometry_cache cache; // Cuidado si este es un enum o struct
        public uint magic;
        public bool owned_by_scene;
        public ufbxi_buf string_buf;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_cache_tmp_channel
    {
        public string name;
        public string interpretation;
        public uint sample_rate;
        public uint start_time;
        public uint end_time;
        public uint current_time;
        public uint consecutive_fails;
        public bool try_load;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_cache_context
    {
        public void* error; // ufbx_error
        public string filename;
        public bool owned_by_scene;
        public bool ignore_if_not_found;
        public ufbx_geometry_cache_opts opts;
        public ufbxi_allocator* ator_tmp;
        public ufbxi_allocator ator_result; // Valor, no puntero
        public ufbxi_buf result;
        public ufbxi_buf tmp;
        public ufbxi_buf tmp_stack;
        public ufbxi_cache_tmp_channel* channels;
        public UIntPtr num_channels;
        public byte* tmp_arr;
        public UIntPtr tmp_arr_size;
        public ufbxi_string_pool string_pool;
        public void* open_file_cb; // ufbx_open_file_cb
        public double frames_per_second;
        public string stream_filename;
        public ufbx_stream stream;
        public bool mc_for8;
        public string xml_filename;
        public uint xml_ticks_per_frame;
        public int xml_type; // ufbxi_cache_xml_type
        public int xml_format; // ufbxi_cache_xml_format
        public string channel_name;
        public byte* name_buf;
        public UIntPtr name_cap;
        public ulong file_offset;
        public byte* pos;
        public byte* pos_end;
        public ufbx_geometry_cache cache;
        public ufbxi_geometry_cache_imp* imp;
        public fixed byte buffer[128];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_cache_interpretation_name
    {
        public int interpretation; // ufbx_cache_interpretation
        public byte* pattern;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_external_file
    {
        public int type; // ufbxi_external_file_type
        public string filename;
        public string absolute_filename;
        public UIntPtr index;
        public void* data;
        public UIntPtr data_size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_anim_layer_combine_ctx
    {
        public void* anim; // const ufbx_anim *
        public void* element; // const ufbx_element *
        public double time;
        public int rotation_order; // ufbx_rotation_order
        public bool has_rotation_order;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_prop_iter
    {
        public void* prop; // const ufbx_prop *
        public void* prop_end;
        public void* over; // const ufbx_prop_override *
        public void* over_end;
        public ufbx_prop tmp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_eval_context
    {
        public byte* src_element;
        public byte* dst_element;
        public ufbxi_scene_imp* src_imp;
        public ufbx_scene src_scene;
        public ufbx_evaluate_opts opts;
        public void* anim; // ufbx_anim *
        public double time;
        public void* error; // ufbx_error
        public ufbxi_allocator ator_result;
        public ufbxi_allocator ator_tmp;
        public ufbxi_buf result;
        public ufbxi_buf tmp;
        public ufbx_scene scene;
        public ufbxi_scene_imp* scene_imp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_create_anim_context
    {
        public void* error; // ufbx_error
        public ufbxi_allocator ator_result;
        public ufbxi_buf result;
        public void* scene; // const ufbx_scene *
        public ufbx_anim_opts opts;
        public ufbx_anim anim;
        public ufbxi_anim_imp* imp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_baked_anim_imp
    {
        public ufbxi_refcount refcount;
        public ufbx_baked_anim bake;
        public uint magic;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_bake_time
    {
        public double time;
        public uint flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_bake_context
    {
        public void* error; // ufbx_error
        public ufbxi_allocator ator_tmp;
        public ufbxi_allocator ator_result;
        public ufbxi_buf result;
        public ufbxi_buf tmp;
        public ufbxi_buf tmp_prop;
        public ufbxi_buf tmp_times;
        public ufbxi_buf tmp_bake_props;
        public ufbxi_buf tmp_nodes;
        public ufbxi_buf tmp_elements;
        public ufbxi_buf tmp_props;
        public ufbxi_buf tmp_bake_stack;

        // ufbxi_bake_time_list se asume struct o pointer
        public ufbxi_bake_time_list layer_weight_times;

        public void** baked_nodes; // ufbx_baked_node **
        public bool* nodes_to_bake;
        public byte* tmp_arr;
        public UIntPtr tmp_arr_size;
        public void* scene; // const ufbx_scene *
        public void* anim; // const ufbx_anim *
        public ufbx_bake_opts opts;
        public double ktime_offset;
        public double time_begin;
        public double time_end;
        public double time_min;
        public double time_max;
        public ufbx_baked_anim bake;
        public ufbxi_baked_anim_imp* imp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_bake_prop
    {
        public uint sort_id;
        public uint element_id;
        public byte* prop_name;
        public void* anim_value; // ufbx_anim_value *
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_line_curve_imp
    {
        public ufbxi_refcount refcount;
        public ufbx_line_curve curve;
        public uint magic;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_tessellate_curve_context
    {
        public void* error; // ufbx_error
        public ufbx_tessellate_curve_opts opts;
        public void* curve; // const ufbx_nurbs_curve *
        public ufbxi_allocator ator_tmp;
        public ufbxi_allocator ator_result;
        public ufbxi_buf result;
        public ufbx_line_curve line;
        public ufbxi_line_curve_imp* imp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_tessellate_surface_context
    {
        public void* error; // ufbx_error
        public ufbx_tessellate_surface_opts opts;
        public void* surface; // const ufbx_nurbs_surface *
        public ufbxi_allocator ator_tmp;
        public ufbxi_allocator ator_result;
        public ufbxi_buf tmp;
        public ufbxi_buf result;
        public ufbxi_map position_map;
        public ufbx_mesh mesh;
        public void* imp; // ufbxi_mesh_imp *
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_kd_node
    {
        public double split; // Asumiendo ufbx_real = double
        public uint index_plus_one;
        public uint slow_left;
        public uint slow_right;
        public uint slow_end;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_ngon_context
    {
        public ufbx_face face;
        public ufbx_vertex_vec3 positions;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public ufbx_vec3[] axes;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1 << (UFBXI_KD_FAST_DEPTH + 1))]
        public ufbxi_kd_node[] kd_nodes;

        public uint* kd_indices;
        public ufbx_vec3 cur_axis_dir;
        public ufbx_face cur_face;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_kd_triangle
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public double[] min_t;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public double[] max_t;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public ufbx_vec2[] points;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] indices;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_subdivide_input
    {
        public void* data;
        public double weight; // ufbx_real
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_subdivide_layer_input
    {
        public void* sum_fn; // ufbxi_subdivide_sum_fn *
        public void* sum_user;
        public void* values;
        public UIntPtr stride;
        public uint* indices;
        public bool check_split_data;
        public bool ignore_indices;
        public int boundary; // ufbx_subdivision_boundary
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_subdivide_layer_output
    {
        public void* values;
        public UIntPtr num_values;
        public uint* indices;
        public UIntPtr num_indices;
        public bool unique_per_vertex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_subdivision_vertex_weights
    {
        public void* weights; // ufbx_subdivision_weight *
        public UIntPtr num_weights;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_subdivide_context
    {
        public void* imp; // ufbxi_mesh_imp *
        public void* error; // ufbx_error
        public void* src_mesh_ptr; // ufbx_mesh *
        public ufbx_mesh src_mesh;
        public ufbx_mesh dst_mesh;
        public void* topo; // ufbx_topo_edge *
        public UIntPtr num_topo;
        public ufbx_subdivide_opts opts;
        public ufbxi_allocator ator_result;
        public ufbxi_allocator ator_tmp;
        public ufbxi_buf result;
        public ufbxi_buf tmp;
        public ufbxi_buf source;
        public ufbxi_subdivide_input* inputs;
        public UIntPtr inputs_cap;
        public double* tmp_vertex_weights; // ufbx_real *
        public void* tmp_weights; // ufbx_subdivision_weight *
        public UIntPtr total_weights;
        public UIntPtr max_vertex_weights;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_vertex_stream
    {
        public byte* begin;
        public byte* ptr;
        public UIntPtr vertex_size;
        public UIntPtr packed_offset;
    }

    // ufbxi_geometry_cache_buffer Union Handler
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct ufbxi_geometry_cache_buffer_src
    {
        [FieldOffset(0)] public fixed double f64[UFBXI_GEOMETRY_CACHE_BUFFER_SIZE];
        [FieldOffset(0)] public fixed float f32[UFBXI_GEOMETRY_CACHE_BUFFER_SIZE];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_geometry_cache_buffer
    {
        public ufbxi_geometry_cache_buffer_src src;
        // Asumiendo ufbx_real = double (C# 'fixed' no soporta typedefs)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UFBXI_GEOMETRY_CACHE_BUFFER_SIZE)]
        public double[] dst;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_buf_padding
    {
        public UIntPtr original_pos;
        public UIntPtr prev_padding;
    }

    // ufbxi_buf_chunk Union Handler
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct ufbxi_buf_chunk_union
    {
        [FieldOffset(0)] public UIntPtr magic;
        [FieldOffset(0)] public void* align_0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_buf_chunk
    {
        public ufbxi_buf_chunk* root;
        public ufbxi_buf_chunk* prev;
        public ufbxi_buf_chunk* next;

        public ufbxi_buf_chunk_union u;

        public UIntPtr size;
        public UIntPtr pushed_pos;
        public UIntPtr next_size;
        public UIntPtr padding_pos;

        public byte data_0; // Flexible array member
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_aa_node
    {
        public ufbxi_aa_node* left;
        public ufbxi_aa_node* right;
        public uint level;
        public uint index;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_task
    {
        public void* data;
        public byte* error; // const char *
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_thread_pool
    {
        public ufbx_thread_opts opts;
        public ufbxi_allocator* ator;
        public void* error; // ufbx_error *
        public void* user_ptr;
        public bool enabled;
        public bool failed;
        public byte* error_desc;
        public uint start_index;
        public uint execute_index;
        public uint wait_index;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UFBX_THREAD_GROUP_COUNT)]
        public ufbxi_task_group[] groups;

        public uint group;
        public uint num_tasks;
        public ufbxi_task_imp* tasks;
    }

    // ufbxi_node Union Handler
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct ufbxi_node_union
    {
        [FieldOffset(0)] public ufbxi_value_array* array;
        [FieldOffset(0)] public void* vals; // ufbxi_value *
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_node
    {
        public byte* name;
        public uint num_children;
        public byte name_len;
        public ushort value_type_mask;
        public ufbxi_node* children;
        public ufbxi_node_union u;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_refcount
    {
        public ufbxi_refcount* parent;
        public void* align_0;
        public uint self_magic;
        public uint type_magic;
        public ufbxi_buf buf;
        public ufbxi_allocator ator;
        public fixed ulong zero_pad_pre[8];
        public ufbxi_atomic_counter refcount;
        public fixed ulong zero_pad_post[8];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_xml_attrib
    {
        public string name;
        public string value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_xml_tag
    {
        public string name;
        public string text;
        public ufbxi_xml_attrib* attribs;
        public UIntPtr num_attribs;
        public ufbxi_xml_tag* children;
        public UIntPtr num_children;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ufbxi_xml_document
    {
        public ufbxi_xml_tag* root;
        public ufbxi_buf buf;
    }








































    // ************************************************ //
    //                  ENUMS NATIVES 1:1               //
    // ************************************************ //


































    // ************************************************ //
    //                     UfbxNative                   //
    // ************************************************ //
    internal static class UfbxNative
    {
        private const string DLL_NAME = "ufbx";

        // --- 3. Funciones exportadas de ufbx.dll ---
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static unsafe extern Scene* ufbx_load_file(string filename, IntPtr opts, IntPtr error);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void ufbx_free_scene(Scene* scene);
    }

    */

}




// Sui recuerda presionar "insert" para sustituir los espacios por letras.
    // ************************************************ //
    //                                                  //
    // ************************************************ //