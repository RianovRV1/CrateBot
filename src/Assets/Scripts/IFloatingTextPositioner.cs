using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IFloatingTextPositioner
{
   bool GetPosition(ref Vector2 postion, GUIContent content, Vector2 size);
}

