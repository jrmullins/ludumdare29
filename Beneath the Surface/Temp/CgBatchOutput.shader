Shader "RagePixel/Basic (Flash)" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_TexWidth ("Texture width", Float) = 128.0
		_TexHeight ("Texture height", Float) = 128.0

	}
	SubShader {
		Tags { "Queue" = "Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }

		Pass {
			ZWrite Off
			Cull off
			Blend SrcAlpha OneMinusSrcAlpha

			Program "vp" {
// Vertex combos: 1
//   opengl - ALU: 7 to 7
//   d3d9 - ALU: 7 to 7
//   d3d11 - ALU: 4 to 4, TEX: 0 to 0, FLOW: 1 to 1
//   d3d11_9x - ALU: 4 to 4, TEX: 0 to 0, FLOW: 1 to 1
SubProgram "opengl " {
Keywords { }
Bind "vertex" Vertex
Bind "color" Color
Bind "texcoord" TexCoord0
"!!ARBvp1.0
# 7 ALU
PARAM c[5] = { { 0 },
		state.matrix.mvp };
MOV result.color, vertex.color;
MOV result.texcoord[0].xy, vertex.texcoord[0];
MOV result.texcoord[0].zw, c[0].x;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 7 instructions, 0 R-regs
"
}

SubProgram "d3d9 " {
Keywords { }
Bind "vertex" Vertex
Bind "color" Color
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
"vs_2_0
; 7 ALU
def c4, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_color0 v1
dcl_texcoord0 v2
mov oD0, v1
mov oT0.xy, v2
mov oT0.zw, c4.x
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}

SubProgram "d3d11 " {
Keywords { }
Bind "vertex" Vertex
Bind "color" Color
Bind "texcoord" TexCoord0
ConstBuffer "UnityPerDraw" 336 // 64 used size, 6 vars
Matrix 0 [glstate_matrix_mvp] 4
BindCB "UnityPerDraw" 0
// 8 instructions, 1 temp regs, 0 temp arrays:
// ALU 4 float, 0 int, 0 uint
// TEX 0 (0 load, 0 comp, 0 bias, 0 grad)
// FLOW 1 static, 0 dynamic
"vs_4_0
eefiecedjkbfopfcnllhfmiaiffmgaopfgbnndjkabaaaaaagiacaaaaadaaaaaa
cmaaaaaajmaaaaaabaabaaaaejfdeheogiaaaaaaadaaaaaaaiaaaaaafaaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaafjaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaafpaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apadaaaafaepfdejfeejepeoaaedepemepfcaafeeffiedepepfceeaaepfdeheo
gmaaaaaaadaaaaaaaiaaaaaafaaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaa
apaaaaaafmaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaapaaaaaagcaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaafdfgfpfagphdgjhegjgpgoaa
edepemepfcaafeeffiedepepfceeaaklfdeieefcfaabaaaaeaaaabaafeaaaaaa
fjaaaaaeegiocaaaaaaaaaaaaeaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaad
pcbabaaaabaaaaaafpaaaaaddcbabaaaacaaaaaaghaaaaaepccabaaaaaaaaaaa
abaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadpccabaaaacaaaaaagiaaaaac
abaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaaegiocaaaaaaaaaaa
abaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaaaaaaaaaaaaaaaaaagbabaaa
aaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaaaaaaaaa
acaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpccabaaaaaaaaaaa
egiocaaaaaaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaaaaaaaaaadgaaaaaf
pccabaaaabaaaaaaegbobaaaabaaaaaadgaaaaafdccabaaaacaaaaaaegbabaaa
acaaaaaadgaaaaaimccabaaaacaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
aaaaaaaadoaaaaab"
}

SubProgram "gles " {
Keywords { }
"!!GLES


#ifdef VERTEX

varying highp vec4 xlv_TEXCOORD0;
varying highp vec4 xlv_COLOR;
uniform highp mat4 glstate_matrix_mvp;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesColor;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.zw = vec2(0.0, 0.0);
  tmpvar_1.xy = _glesMultiTexCoord0.xy;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_COLOR = _glesColor;
  xlv_TEXCOORD0 = tmpvar_1;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD0;
varying highp vec4 xlv_COLOR;
uniform highp float _TexHeight;
uniform highp float _TexWidth;
uniform sampler2D _MainTex;
void main ()
{
  mediump vec4 texcol_1;
  highp vec2 tmpvar_2;
  tmpvar_2.x = ((float(int((xlv_TEXCOORD0.x * _TexWidth))) + 0.5) / _TexWidth);
  tmpvar_2.y = ((float(int((xlv_TEXCOORD0.y * _TexHeight))) + 0.5) / _TexHeight);
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, tmpvar_2);
  texcol_1 = tmpvar_3;
  gl_FragData[0] = (texcol_1 * xlv_COLOR);
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { }
"!!GLES


#ifdef VERTEX

varying highp vec4 xlv_TEXCOORD0;
varying highp vec4 xlv_COLOR;
uniform highp mat4 glstate_matrix_mvp;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesColor;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.zw = vec2(0.0, 0.0);
  tmpvar_1.xy = _glesMultiTexCoord0.xy;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_COLOR = _glesColor;
  xlv_TEXCOORD0 = tmpvar_1;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD0;
varying highp vec4 xlv_COLOR;
uniform highp float _TexHeight;
uniform highp float _TexWidth;
uniform sampler2D _MainTex;
void main ()
{
  mediump vec4 texcol_1;
  highp vec2 tmpvar_2;
  tmpvar_2.x = ((float(int((xlv_TEXCOORD0.x * _TexWidth))) + 0.5) / _TexWidth);
  tmpvar_2.y = ((float(int((xlv_TEXCOORD0.y * _TexHeight))) + 0.5) / _TexHeight);
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, tmpvar_2);
  texcol_1 = tmpvar_3;
  gl_FragData[0] = (texcol_1 * xlv_COLOR);
}



#endif"
}

SubProgram "flash " {
Keywords { }
Bind "vertex" Vertex
Bind "color" Color
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
"agal_vs
c4 0.0 0.0 0.0 0.0
[bc]
aaaaaaaaahaaapaeacaaaaoeaaaaaaaaaaaaaaaaaaaaaaaa mov v7, a2
aaaaaaaaaaaaadaeadaaaaoeaaaaaaaaaaaaaaaaaaaaaaaa mov v0.xy, a3
aaaaaaaaaaaaamaeaeaaaaaaabaaaaaaaaaaaaaaaaaaaaaa mov v0.zw, c4.x
bdaaaaaaaaaaaiadaaaaaaoeaaaaaaaaadaaaaoeabaaaaaa dp4 o0.w, a0, c3
bdaaaaaaaaaaaeadaaaaaaoeaaaaaaaaacaaaaoeabaaaaaa dp4 o0.z, a0, c2
bdaaaaaaaaaaacadaaaaaaoeaaaaaaaaabaaaaoeabaaaaaa dp4 o0.y, a0, c1
bdaaaaaaaaaaabadaaaaaaoeaaaaaaaaaaaaaaoeabaaaaaa dp4 o0.x, a0, c0
"
}

SubProgram "d3d11_9x " {
Keywords { }
Bind "vertex" Vertex
Bind "color" Color
Bind "texcoord" TexCoord0
ConstBuffer "UnityPerDraw" 336 // 64 used size, 6 vars
Matrix 0 [glstate_matrix_mvp] 4
BindCB "UnityPerDraw" 0
// 8 instructions, 1 temp regs, 0 temp arrays:
// ALU 4 float, 0 int, 0 uint
// TEX 0 (0 load, 0 comp, 0 bias, 0 grad)
// FLOW 1 static, 0 dynamic
"vs_4_0_level_9_1
eefiecedfcmjicbhkklhakiklondbpmddilcijdeabaaaaaaheadaaaaaeaaaaaa
daaaaaaadiabaaaajaacaaaaaaadaaaaebgpgodjaaabaaaaaaabaaaaaaacpopp
mmaaaaaadeaaaaaaabaaceaaaaaadaaaaaaadaaaaaaaceaaabaadaaaaaaaaaaa
aeaaabaaaaaaaaaaaaaaaaaaaaacpoppfbaaaaafafaaapkaaaaaiadpaaaaaaaa
aaaaaaaaaaaaaaaabpaaaaacafaaaaiaaaaaapjabpaaaaacafaaabiaabaaapja
bpaaaaacafaaaciaacaaapjaafaaaaadaaaaapiaaaaaffjaacaaoekaaeaaaaae
aaaaapiaabaaoekaaaaaaajaaaaaoeiaaeaaaaaeaaaaapiaadaaoekaaaaakkja
aaaaoeiaaeaaaaaeaaaaapiaaeaaoekaaaaappjaaaaaoeiaaeaaaaaeaaaaadma
aaaappiaaaaaoekaaaaaoeiaabaaaaacaaaaammaaaaaoeiaabaaaaacaaaaapoa
abaaoejaafaaaaadabaaapoaacaaaejaafaafakappppaaaafdeieefcfaabaaaa
eaaaabaafeaaaaaafjaaaaaeegiocaaaaaaaaaaaaeaaaaaafpaaaaadpcbabaaa
aaaaaaaafpaaaaadpcbabaaaabaaaaaafpaaaaaddcbabaaaacaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadpccabaaa
acaaaaaagiaaaaacabaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaa
egiocaaaaaaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaaaaaaaaa
aaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaa
egiocaaaaaaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaak
pccabaaaaaaaaaaaegiocaaaaaaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaa
aaaaaaaadgaaaaafpccabaaaabaaaaaaegbobaaaabaaaaaadgaaaaafdccabaaa
acaaaaaaegbabaaaacaaaaaadgaaaaaimccabaaaacaaaaaaaceaaaaaaaaaaaaa
aaaaaaaaaaaaaaaaaaaaaaaadoaaaaabejfdeheogiaaaaaaadaaaaaaaiaaaaaa
faaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaafjaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaabaaaaaaapapaaaafpaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
acaaaaaaapadaaaafaepfdejfeejepeoaaedepemepfcaafeeffiedepepfceeaa
epfdeheogmaaaaaaadaaaaaaaiaaaaaafaaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaafmaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaapaaaaaa
gcaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaafdfgfpfagphdgjhe
gjgpgoaaedepemepfcaafeeffiedepepfceeaakl"
}

SubProgram "gles3 " {
Keywords { }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;

#line 151
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 187
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 181
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 318
struct VertOut {
    highp vec4 position;
    highp vec4 color;
    highp vec4 texcoord;
};
#line 325
struct VertIn {
    highp vec4 vertex;
    highp vec4 color;
    highp vec4 texcoord;
};
#line 340
struct FragOut {
    highp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[8];
uniform highp vec4 unity_LightPosition[8];
uniform highp vec4 unity_LightAtten[8];
#line 19
uniform highp vec4 unity_SpotDirection[8];
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
#line 23
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
#line 27
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
#line 31
uniform highp vec4 _LightSplitsNear;
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
#line 35
uniform highp vec4 unity_ShadowFadeCenterAndType;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
#line 39
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
#line 43
uniform highp mat4 glstate_matrix_texture0;
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
#line 47
uniform highp mat4 glstate_matrix_projection;
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
#line 51
uniform lowp vec4 unity_ColorSpaceGrey;
#line 77
#line 82
#line 87
#line 91
#line 96
#line 120
#line 137
#line 158
#line 166
#line 193
#line 206
#line 215
#line 220
#line 229
#line 234
#line 243
#line 260
#line 265
#line 291
#line 299
#line 307
#line 311
#line 315
uniform sampler2D _MainTex;
uniform highp float _TexWidth;
uniform highp float _TexHeight;
#line 332
#line 345
#line 332
VertOut vert( in VertIn xlat_varinput ) {
    VertOut xlat_varoutput;
    xlat_varoutput.position = (glstate_matrix_mvp * xlat_varinput.vertex);
    #line 336
    xlat_varoutput.color = xlat_varinput.color;
    xlat_varoutput.texcoord = vec4( xlat_varinput.texcoord.xy, 0.0, 0.0);
    return xlat_varoutput;
}
out highp vec4 xlv_COLOR;
out highp vec4 xlv_TEXCOORD0;
void main() {
    VertOut xl_retval;
    VertIn xlt_xlat_varinput;
    xlt_xlat_varinput.vertex = vec4(gl_Vertex);
    xlt_xlat_varinput.color = vec4(gl_Color);
    xlt_xlat_varinput.texcoord = vec4(gl_MultiTexCoord0);
    xl_retval = vert( xlt_xlat_varinput);
    gl_Position = vec4(xl_retval.position);
    xlv_COLOR = vec4(xl_retval.color);
    xlv_TEXCOORD0 = vec4(xl_retval.texcoord);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 151
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 187
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 181
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 318
struct VertOut {
    highp vec4 position;
    highp vec4 color;
    highp vec4 texcoord;
};
#line 325
struct VertIn {
    highp vec4 vertex;
    highp vec4 color;
    highp vec4 texcoord;
};
#line 340
struct FragOut {
    highp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[8];
uniform highp vec4 unity_LightPosition[8];
uniform highp vec4 unity_LightAtten[8];
#line 19
uniform highp vec4 unity_SpotDirection[8];
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
#line 23
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
#line 27
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
#line 31
uniform highp vec4 _LightSplitsNear;
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
#line 35
uniform highp vec4 unity_ShadowFadeCenterAndType;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
#line 39
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
#line 43
uniform highp mat4 glstate_matrix_texture0;
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
#line 47
uniform highp mat4 glstate_matrix_projection;
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
#line 51
uniform lowp vec4 unity_ColorSpaceGrey;
#line 77
#line 82
#line 87
#line 91
#line 96
#line 120
#line 137
#line 158
#line 166
#line 193
#line 206
#line 215
#line 220
#line 229
#line 234
#line 243
#line 260
#line 265
#line 291
#line 299
#line 307
#line 311
#line 315
uniform sampler2D _MainTex;
uniform highp float _TexWidth;
uniform highp float _TexHeight;
#line 332
#line 345
#line 345
FragOut frag( in VertIn xlat_varinput ) {
    FragOut xlat_varoutput;
    mediump vec4 texcol = texture( _MainTex, vec2( ((float(int((xlat_varinput.texcoord.x * _TexWidth))) + 0.5) / _TexWidth), ((float(int((xlat_varinput.texcoord.y * _TexHeight))) + 0.5) / _TexHeight)));
    #line 349
    xlat_varoutput.color = (texcol * xlat_varinput.color);
    return xlat_varoutput;
}
in highp vec4 xlv_COLOR;
in highp vec4 xlv_TEXCOORD0;
void main() {
    FragOut xl_retval;
    VertIn xlt_xlat_varinput;
    xlt_xlat_varinput.vertex = vec4(0.0);
    xlt_xlat_varinput.color = vec4(xlv_COLOR);
    xlt_xlat_varinput.texcoord = vec4(xlv_TEXCOORD0);
    xl_retval = frag( xlt_xlat_varinput);
    gl_FragData[0] = vec4(xl_retval.color);
}


#endif"
}

}
Program "fp" {
// Fragment combos: 1
//   opengl - ALU: 18 to 18, TEX: 1 to 1
//   d3d9 - ALU: 18 to 18, TEX: 1 to 1
//   d3d11 - ALU: 5 to 5, TEX: 1 to 1, FLOW: 1 to 1
//   d3d11_9x - ALU: 5 to 5, TEX: 1 to 1, FLOW: 1 to 1
SubProgram "opengl " {
Keywords { }
Float 0 [_TexWidth]
Float 1 [_TexHeight]
SetTexture 0 [_MainTex] 2D
"!!ARBfp1.0
# 18 ALU, 1 TEX
PARAM c[3] = { program.local[0..1],
		{ 0, 0.5 } };
TEMP R0;
MUL R0.x, fragment.texcoord[0].y, c[1];
ABS R0.y, R0.x;
FLR R0.y, R0;
SLT R0.x, R0, c[2];
CMP R0.x, -R0, -R0.y, R0.y;
ADD R0.w, R0.x, c[2].y;
MUL R0.x, fragment.texcoord[0], c[0];
ABS R0.z, R0.x;
RCP R0.y, c[1].x;
SLT R0.x, R0, c[2];
FLR R0.z, R0;
CMP R0.z, -R0.x, -R0, R0;
ADD R0.z, R0, c[2].y;
RCP R0.x, c[0].x;
MUL R0.y, R0.w, R0;
MUL R0.x, R0.z, R0;
TEX R0, R0, texture[0], 2D;
MUL result.color, R0, fragment.color.primary;
END
# 18 instructions, 1 R-regs
"
}

SubProgram "d3d9 " {
Keywords { }
Float 0 [_TexWidth]
Float 1 [_TexHeight]
SetTexture 0 [_MainTex] 2D
"ps_2_0
; 18 ALU, 1 TEX
dcl_2d s0
def c2, 0.50000000, 0, 0, 0
dcl v0
dcl t0.xy
mul r2.x, t0.y, c1
mul r0.x, t0, c0
abs r4.x, r2
abs r1.x, r0
frc r3.x, r1
add r1.x, r1, -r3
cmp r1.x, r0, r1, -r1
frc r5.x, r4
add r4.x, r4, -r5
cmp r3.x, r2, r4, -r4
rcp r2.x, c1.x
add r3.x, r3, c2
mul r0.y, r3.x, r2.x
rcp r0.x, c0.x
add r1.x, r1, c2
mul r0.x, r1, r0
texld r0, r0, s0
mul r0, r0, v0
mov oC0, r0
"
}

SubProgram "d3d11 " {
Keywords { }
ConstBuffer "$Globals" 32 // 24 used size, 3 vars
Float 16 [_TexWidth]
Float 20 [_TexHeight]
BindCB "$Globals" 0
SetTexture 0 [_MainTex] 2D 0
// 7 instructions, 1 temp regs, 0 temp arrays:
// ALU 5 float, 0 int, 0 uint
// TEX 1 (0 load, 0 comp, 0 bias, 0 grad)
// FLOW 1 static, 0 dynamic
"ps_4_0
eefiecedlebaelbhmkocffolcjdlaaelmgaehikpabaaaaaapmabaaaaadaaaaaa
cmaaaaaakaaaaaaaneaaaaaaejfdeheogmaaaaaaadaaaaaaaiaaaaaafaaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaafmaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaagcaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apadaaaafdfgfpfagphdgjhegjgpgoaaedepemepfcaafeeffiedepepfceeaakl
epfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
aaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefccaabaaaaeaaaaaaa
eiaaaaaafjaaaaaeegiocaaaaaaaaaaaacaaaaaafkaaaaadaagabaaaaaaaaaaa
fibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaadpcbabaaaabaaaaaagcbaaaad
dcbabaaaacaaaaaagfaaaaadpccabaaaaaaaaaaagiaaaaacabaaaaaadiaaaaai
dcaabaaaaaaaaaaaegbabaaaacaaaaaaegiacaaaaaaaaaaaabaaaaaaedaaaaaf
dcaabaaaaaaaaaaaegaabaaaaaaaaaaaaaaaaaakdcaabaaaaaaaaaaaegaabaaa
aaaaaaaaaceaaaaaaaaaaadpaaaaaadpaaaaaaaaaaaaaaaaaoaaaaaidcaabaaa
aaaaaaaaegaabaaaaaaaaaaaegiacaaaaaaaaaaaabaaaaaaefaaaaajpcaabaaa
aaaaaaaaegaabaaaaaaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaadiaaaaah
pccabaaaaaaaaaaaegaobaaaaaaaaaaaegbobaaaabaaaaaadoaaaaab"
}

SubProgram "gles " {
Keywords { }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { }
"!!GLES"
}

SubProgram "flash " {
Keywords { }
Float 0 [_TexWidth]
Float 1 [_TexHeight]
SetTexture 0 [_MainTex] 2D
"agal_ps
c2 0.5 0.0 0.0 0.0
[bc]
adaaaaaaacaaabacaaaaaaffaeaaaaaaabaaaaoeabaaaaaa mul r2.x, v0.y, c1
adaaaaaaaaaaabacaaaaaaoeaeaaaaaaaaaaaaoeabaaaaaa mul r0.x, v0, c0
beaaaaaaaeaaabacacaaaaaaacaaaaaaaaaaaaaaaaaaaaaa abs r4.x, r2.x
beaaaaaaabaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa abs r1.x, r0.x
aiaaaaaaadaaabacabaaaaaaacaaaaaaaaaaaaaaaaaaaaaa frc r3.x, r1.x
acaaaaaaabaaabacabaaaaaaacaaaaaaadaaaaaaacaaaaaa sub r1.x, r1.x, r3.x
bfaaaaaaabaaacacabaaaaaaacaaaaaaaaaaaaaaaaaaaaaa neg r1.y, r1.x
ckaaaaaaacaaacacaaaaaaaaacaaaaaaacaaaaffabaaaaaa slt r2.y, r0.x, c2.y
acaaaaaaadaaacacabaaaaffacaaaaaaabaaaaaaacaaaaaa sub r3.y, r1.y, r1.x
adaaaaaaadaaacacadaaaaffacaaaaaaacaaaaffacaaaaaa mul r3.y, r3.y, r2.y
abaaaaaaabaaabacadaaaaffacaaaaaaabaaaaaaacaaaaaa add r1.x, r3.y, r1.x
aiaaaaaaafaaabacaeaaaaaaacaaaaaaaaaaaaaaaaaaaaaa frc r5.x, r4.x
acaaaaaaaeaaabacaeaaaaaaacaaaaaaafaaaaaaacaaaaaa sub r4.x, r4.x, r5.x
bfaaaaaaaeaaacacaeaaaaaaacaaaaaaaaaaaaaaaaaaaaaa neg r4.y, r4.x
ckaaaaaaafaaabacacaaaaaaacaaaaaaacaaaaffabaaaaaa slt r5.x, r2.x, c2.y
acaaaaaaafaaacacaeaaaaffacaaaaaaaeaaaaaaacaaaaaa sub r5.y, r4.y, r4.x
adaaaaaaadaaabacafaaaaffacaaaaaaafaaaaaaacaaaaaa mul r3.x, r5.y, r5.x
abaaaaaaadaaabacadaaaaaaacaaaaaaaeaaaaaaacaaaaaa add r3.x, r3.x, r4.x
aaaaaaaaaeaaapacabaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov r4, c1
afaaaaaaacaaabacaeaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rcp r2.x, r4.x
abaaaaaaadaaabacadaaaaaaacaaaaaaacaaaaoeabaaaaaa add r3.x, r3.x, c2
adaaaaaaaaaaacacadaaaaaaacaaaaaaacaaaaaaacaaaaaa mul r0.y, r3.x, r2.x
aaaaaaaaacaaapacaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov r2, c0
afaaaaaaaaaaabacacaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rcp r0.x, r2.x
abaaaaaaabaaabacabaaaaaaacaaaaaaacaaaaoeabaaaaaa add r1.x, r1.x, c2
adaaaaaaaaaaabacabaaaaaaacaaaaaaaaaaaaaaacaaaaaa mul r0.x, r1.x, r0.x
ciaaaaaaaaaaapacaaaaaafeacaaaaaaaaaaaaaaafaababb tex r0, r0.xyyy, s0 <2d wrap linear point>
adaaaaaaaaaaapacaaaaaaoeacaaaaaaahaaaaoeaeaaaaaa mul r0, r0, v7
aaaaaaaaaaaaapadaaaaaaoeacaaaaaaaaaaaaaaaaaaaaaa mov o0, r0
"
}

SubProgram "d3d11_9x " {
Keywords { }
ConstBuffer "$Globals" 32 // 24 used size, 3 vars
Float 16 [_TexWidth]
Float 20 [_TexHeight]
BindCB "$Globals" 0
SetTexture 0 [_MainTex] 2D 0
// 7 instructions, 1 temp regs, 0 temp arrays:
// ALU 5 float, 0 int, 0 uint
// TEX 1 (0 load, 0 comp, 0 bias, 0 grad)
// FLOW 1 static, 0 dynamic
"ps_4_0_level_9_1
eefiecednaeignnjihdchbljgjmlegnpahnmldmjabaaaaaammadaaaaaeaaaaaa
daaaaaaapmabaaaaceadaaaajiadaaaaebgpgodjmeabaaaameabaaaaaaacpppp
jaabaaaadeaaaaaaabaaciaaaaaadeaaaaaadeaaabaaceaaaaaadeaaaaaaaaaa
aaaaabaaabaaaaaaaaaaaaaaaaacppppfbaaaaafabaaapkaaaaaaaaaaaaaiadp
aaaaaadpaaaaaaaabpaaaaacaaaaaaiaaaaaaplabpaaaaacaaaaaaiaabaaapla
bpaaaaacaaaaaajaaaaiapkaafaaaaadaaaaaiiaabaaaalaaaaaaakabdaaaaac
aaaaabiaaaaappiafiaaaaaeaaaaaciaaaaaaaibabaaaakaabaaffkaacaaaaad
aaaaabiaaaaappiaaaaaaaibfiaaaaaeaaaaaciaaaaappiaabaaaakaaaaaffia
acaaaaadaaaaabiaaaaaffiaaaaaaaiaacaaaaadaaaaabiaaaaaaaiaabaakkka
agaaaaacaaaaaciaaaaaaakaafaaaaadaaaaabiaaaaaffiaaaaaaaiaafaaaaad
aaaaaeiaabaafflaaaaaffkabdaaaaacaaaaaiiaaaaakkiafiaaaaaeabaaaiia
aaaappibabaaaakaabaaffkaacaaaaadaaaaaiiaaaaakkiaaaaappibfiaaaaae
aaaaaeiaaaaakkiaabaaaakaabaappiaacaaaaadaaaaaeiaaaaakkiaaaaappia
acaaaaadaaaaaeiaaaaakkiaabaakkkaagaaaaacaaaaaiiaaaaaffkaafaaaaad
aaaaaciaaaaappiaaaaakkiaecaaaaadaaaacpiaaaaaoeiaaaaioekaafaaaaad
aaaaapiaaaaaoeiaaaaaoelaabaaaaacaaaiapiaaaaaoeiappppaaaafdeieefc
caabaaaaeaaaaaaaeiaaaaaafjaaaaaeegiocaaaaaaaaaaaacaaaaaafkaaaaad
aagabaaaaaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaadpcbabaaa
abaaaaaagcbaaaaddcbabaaaacaaaaaagfaaaaadpccabaaaaaaaaaaagiaaaaac
abaaaaaadiaaaaaidcaabaaaaaaaaaaaegbabaaaacaaaaaaegiacaaaaaaaaaaa
abaaaaaaedaaaaafdcaabaaaaaaaaaaaegaabaaaaaaaaaaaaaaaaaakdcaabaaa
aaaaaaaaegaabaaaaaaaaaaaaceaaaaaaaaaaadpaaaaaadpaaaaaaaaaaaaaaaa
aoaaaaaidcaabaaaaaaaaaaaegaabaaaaaaaaaaaegiacaaaaaaaaaaaabaaaaaa
efaaaaajpcaabaaaaaaaaaaaegaabaaaaaaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaadiaaaaahpccabaaaaaaaaaaaegaobaaaaaaaaaaaegbobaaaabaaaaaa
doaaaaabejfdeheogmaaaaaaadaaaaaaaiaaaaaafaaaaaaaaaaaaaaaabaaaaaa
adaaaaaaaaaaaaaaapaaaaaafmaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
apapaaaagcaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaaapadaaaafdfgfpfa
gphdgjhegjgpgoaaedepemepfcaafeeffiedepepfceeaaklepfdeheocmaaaaaa
abaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapaaaaaa
fdfgfpfegbhcghgfheaaklkl"
}

SubProgram "gles3 " {
Keywords { }
"!!GLES3"
}

}

#LINE 61

		}

	} 
	FallBack "Diffuse"
}
