﻿using Windows.UI.Xaml;
using FroggerStarter.View.Sprites.Vehicles;

namespace FroggerStarter.Model.v
{
    /// <summary>
    /// Initializes Super car sprite and handles functionality
    /// </summary>
    /// <seealso cref="Vehicle" />
    public class SuperCar : Vehicle
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="SuperCar" /> class.
        ///     Precondition: None
        ///     Postcondition: A SuperCar object is created
        /// </summary>
        /// <param name="direction">The Direction.</param>
        /// <param name="speed">The speed.</param>
        public SuperCar(VehicleDirection direction, int speed) :
            base(direction, speed)
        {
            this.initializeSuperCarSprite();
        }

        private void initializeSuperCarSprite()
        {
            Sprite = new SuperCarSprite
            {
                Visibility = Visibility.Collapsed
            };
        }
    }
}
