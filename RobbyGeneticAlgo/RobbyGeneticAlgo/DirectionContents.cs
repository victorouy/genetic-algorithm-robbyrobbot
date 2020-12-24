using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbyGeneticAlgo
{

    /*
      * @Author: Rahul Anton and Victor Ouy
      * @Date: 13/04/2020
      * 
      * 
      * this struct is used to get the surrounding tiles
      */
    public struct DirectionContents
    {
        public Contents N { get; set; }
        public Contents S { get; set; }
        public Contents E { get; set; }
        public Contents W { get; set; }
        public Contents Current { get; set; }


        /*
      * @Author: Rahul Anton and Victor Ouy
      * @Date: 13/04/2020
      * 
      * @Param: N, S, E, W, Current
      * 
      * Constructor for the DirectionsContents struct
      */
        public DirectionContents(Contents N, Contents S, Contents E, Contents W, Contents Current)
        {
            this.N = N;
            this.S = S;
            this.E = E;
            this.W = W;
            this.Current = Current;
        }
    }
}
