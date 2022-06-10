Shader "Custom/Grid" {
    
    Properties {
        _color ("Color", Color) = (.255,.0,.0,1)
        _cellSize ("Cell Size", Range(0.01, 1.0)) = 0.1
        _thickness ("Thickness", Range(0.00001, 0.5)) = 0.05
    }
    
    SubShader {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        
        Pass {
            CGPROGRAM
            #pragma vertex vertex
            #pragma fragment fragment
            
            struct vertexInput {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct vertexOutput {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 positionInWorldSpace : TEXCOORD1;
            };

            float4 _color;
            float _cellSize;
            float _thickness;
            float4 _occupiedCells[1024];
            static const float _bufferStopW = -1.0;
            
            float drawGrid(float2 worldPosition);
            float drawLines(float2 worldPosition);
            float drawOccupiedCells(float2 worldPosition);
            bool isWithinBounds(float2 position, float2 bottomLeft, float2 topRight);
            
            float drawGrid(float2 worldPosition) {
                const float lines = drawLines(worldPosition);

                if (lines) {
                    return lines;
                }

                return drawOccupiedCells(worldPosition);
            }

            float drawLines(float2 worldPosition) {
                float2 cellPosition = worldPosition % _cellSize;
                float result = abs(cellPosition.x) < _thickness;
                result += abs(cellPosition.y) < _thickness;

                return result;
            }

            float drawOccupiedCells(float2 worldPosition) {
                for (int i = 0; i < 1024; i++) {
                    const float4 cell = _occupiedCells[i];

                    if (cell.w == _bufferStopW) {
                        break;
                    }
                    
                    const float2 bottomLeft = float2(
                        _cellSize * (cell.x + (cell.x > 0.0 ? - 1.0 : 0.0)),
                        _cellSize * (cell.y + (cell.y > 0.0 ? - 1.0 : 0.0))
                    );
                    
                    const float2 topRight = bottomLeft + _cellSize;
                    
                    if (isWithinBounds(worldPosition, bottomLeft, topRight)) {
                        return 1.0;
                    }
                }

                return 0.0;
            }

            fixed4 fragment(vertexOutput input) : SV_Target {
                const float grid = drawGrid(input.positionInWorldSpace);
                
                if (grid == 0.0) {
                    return float4(0.0, 0.0, 0.0, 0.0);
                }

                return _color * grid;
            }

            bool isWithinBounds(float2 position, float2 bottomLeft, float2 topRight) {
                if (position.x < bottomLeft.x || position.x > topRight.x) {
                    return false;
                }

                if (position.y < bottomLeft.y || position.y > topRight.y) {
                    return false;
                }

                return true;
            }

            vertexOutput vertex (vertexInput input) {
                vertexOutput output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.uv = input.uv;
                output.positionInWorldSpace = mul(unity_ObjectToWorld, input.vertex);
                
                return output;
            }
            
            ENDCG
        }
    }
}
