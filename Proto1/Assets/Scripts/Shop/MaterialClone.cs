using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


    [System.Serializable]
    internal class MaterialClone : Material
    {
        public MaterialClone(Shader shader) : base(shader)
        {
        }

        public MaterialClone(Material source) : base(source)
        {
        }

        public MaterialClone(string contents) : base(contents)
        {
        }

        public override bool Equals(object other)
        {
            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    
}
