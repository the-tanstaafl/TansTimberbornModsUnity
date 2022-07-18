// using Timberborn.BlockSystem;
// using UnityEditor;
// using UnityEngine;
//
// namespace Ladder {
//   [CustomPropertyDrawer(typeof(BlockSpecification))]
//   public class BlockPropertyDrawer : PropertyDrawer {
//
//     private static readonly int NumberOfProperties = 4;
//
//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
//       EditorGUI.BeginProperty(position, label, property);
//
//       AddPropertyField(position, property, "_matterBelow", 0);
//       AddPropertyField(position, property, "_occupation", 1);
//       AddPropertyField(position, property, "_stackable", 2);
//       AddPropertyField(position, property, "_optionallyUnderground", 3);
//
//       var oldLabelWidth = EditorGUIUtility.labelWidth;
//       EditorGUIUtility.labelWidth = 70.0f;
//
//       EditorGUIUtility.labelWidth = oldLabelWidth;
//
//       EditorGUI.EndProperty();
//     }
//
//     private void AddPropertyField(Rect position, SerializedProperty serializedProperty,
//                                   string name, int index) {
//       var property = serializedProperty.FindPropertyRelative(name);
//       var rect = new Rect(position.x + position.width / NumberOfProperties * index, position.y,
//                           position.width / NumberOfProperties, position.height);
//       EditorGUI.PropertyField(rect, property, GUIContent.none);
//     }
//
//   }
// }
//
// namespace Ladder {
//   [CustomPropertyDrawer(typeof(BlocksSpecification))]
//   public class BlocksPropertyDrawer : PropertyDrawer {
//
//     private static readonly int BlockLabelWidth = 80;
//     private static readonly float BlockHeight = EditorGUIUtility.singleLineHeight;
//     private static readonly int ColumnsDescriptionHeight = 15;
//     private static readonly string[] ColumnLabels = {
//         "Matter below", "Occupation", "Stackable", "Optionally underground"
//     };
//
//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
//       EditorGUI.BeginProperty(position, label, property);
//
//       var sizeProperty = property.FindPropertyRelative("_size");
//       var blocksProperty = property.FindPropertyRelative("_blockSpecifications");
//
//       var size = sizeProperty.vector3IntValue;
//
//       var expectedBlocksSize = size.x * size.y * size.z;
//       if (blocksProperty.arraySize != expectedBlocksSize) {
//         blocksProperty.arraySize = expectedBlocksSize;
//       }
//
//       EditorGUI.PropertyField(position, sizeProperty);
//       var sizePropertyHeight = EditorGUI.GetPropertyHeight(sizeProperty);
// for (var i = 0; i < ColumnLabels.Length; i++) {
//         GUI.Label(
//             new(position.x
//                 + BlockLabelWidth
//                 + (position.width - BlockLabelWidth) / ColumnLabels.Length * i,
//                 position.y + sizePropertyHeight,
//                 (position.width - BlockLabelWidth) / ColumnLabels.Length,
//                 ColumnsDescriptionHeight),
//             ColumnLabels[i]);
//       }
//
//       var coords = new Vector3Int();
//       var blockDisplayIndex = 0;
//       for (coords.z = 0; coords.z < size.z; ++coords.z) {
//         for (coords.x = 0; coords.x < size.x; ++coords.x) {
//           for (coords.y = 0; coords.y < size.y; ++coords.y) {
//             var blockLabelText = coords.x + ", " + coords.y + ", " + coords.z;
//             var blockIndex = IndexFromCoordinates(coords, size);
//             var blockLabelRect = new Rect(position.x,
//                                           position.y
//                                           + blockDisplayIndex * BlockHeight
//                                           + sizePropertyHeight
//                                           + ColumnsDescriptionHeight,
//                                           BlockLabelWidth, BlockHeight);
//             var blockRect = new Rect(position.x + BlockLabelWidth,
//                                      position.y
//                                      + blockDisplayIndex * BlockHeight
//                                      + sizePropertyHeight
//                                      + ColumnsDescriptionHeight,
//                                      position.width - BlockLabelWidth, BlockHeight);
//
//             GUI.Label(blockLabelRect, blockLabelText);
//             EditorGUI.PropertyField(blockRect, blocksProperty.GetArrayElementAtIndex(blockIndex));
//
//             ++blockDisplayIndex;
//           }
//         }
//       }
//
//       EditorGUI.EndProperty();
//     }
// public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
//       var sizeProperty = property.FindPropertyRelative("_size");
//       var sizePropertyHeight = EditorGUI.GetPropertyHeight(sizeProperty);
//       var size = sizeProperty.vector3IntValue;
//       var numberOfBlocks = size.x * size.y * size.z;
//       return numberOfBlocks * BlockHeight + sizePropertyHeight + ColumnsDescriptionHeight;
//     }
//
//     private int IndexFromCoordinates(Vector3Int coordinates, Vector3Int size) {
//       return (coordinates.z * size.y + coordinates.y) * size.x + coordinates.x;
//     }
//
//   }
// }