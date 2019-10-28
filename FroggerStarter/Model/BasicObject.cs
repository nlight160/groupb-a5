using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FroggerStarter.Model
{
    /// <summary>
    /// Basic object that defines an objects size and location in the game
    /// </summary>
    public abstract class BasicObject
    {

        private Point location;

        /// <summary>
        ///     Gets or sets the x location of the game object.
        /// </summary>
        /// <value>
        ///     The x.
        /// </value>
        public double X
        {
            get => this.location.X;
            set
            {
                this.location.X = (int) value;
                this.render();
            }
        }

        /// <summary>
        ///     Gets or sets the y location of the game object.
        /// </summary>
        /// <value>
        ///     The y.
        /// </value>
        public double Y
        {
            get => this.location.Y;
            set
            {
                this.location.Y = (int) value; //TODO dont know why this needed a cast
                this.render();
            }
        }

        /// <summary>
        ///     Gets the width of the game object.
        /// </summary>
        /// <value>
        ///     The width.
        /// </value>
        public double Width => this.Sprite.Width;

        /// <summary>
        ///     Gets the height of the game object.
        /// </summary>
        /// <value>
        ///     The height.
        /// </value>
        public double Height => this.Sprite.Height;

        /// <summary>
        ///     Gets or sets the sprite associated with the game object.
        /// </summary>
        /// <value>
        ///     The sprite.
        /// </value>
        public BaseSprite Sprite { get; protected set; }

        /// <summary>
        /// Gets the bounding box.
        /// </summary>
        /// <value>
        /// The bounding box.
        /// </value>
        public Rectangle BoundingBox => new Rectangle((int)this.X, (int)this.Y, (int)this.Sprite.Width, (int)this.Sprite.Height);


        private void render()
        {
            this.Sprite.RenderAt(this.X, this.Y);
        }

    }
}
