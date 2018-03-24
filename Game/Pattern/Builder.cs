using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    abstract class Builder
    {
        private string res;

        public string Res { get => res; set => res = value; }

        public abstract void Build();
        public abstract string Result();
    }
    class HeavyBuilder: Builder
    {
        public override void Build()
        {
            Res = "Тяжелый";
        }
        public override string Result()
        {
            return Res;
        }
    }
    class MagicBuilder : Builder
    {
        public override void Build()
        {
            Res = "Магический";
        }
        public override string Result()
        {
            return Res;
        }
    }

    }
