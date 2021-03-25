﻿Shader "Custom/3D Mask"
{
    SubShader
    {
        Tags { "Queue"="AlphaTest+51" }
        ColorMask 0
        
        Pass 
        {
            Stencil 
            {
                Ref 0
                Comp Always
                Pass Replace
            }
            
            Blend Zero One
            ZWrite Off
        }
    }
}
