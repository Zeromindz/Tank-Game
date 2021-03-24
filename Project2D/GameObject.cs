using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLibrary;
using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
	class GameObject
	{
		//Game objects
		protected GameObject m_Parent = null;
		protected List<GameObject> m_Children = new List<GameObject>();
		
		//Matrices
		protected Matrix3 m_LocalTransform = new Matrix3(true);
		protected Matrix3 m_GlobalTransfrom;


		//Drawing
		protected Image m_Image;
		protected Texture2D m_Texture;

		//Collision
		protected float m_ColRadius = 0.0f;
		public Vector2 m_PreviousPos;
		// m1 - m4 - m7
		// m2 - m5 - m8
		// m3 - m6 - m9

		public GameObject(string _fileName)
		{
			//load image and convert to texture
			m_Image = LoadImage(_fileName);
			m_Texture = LoadTextureFromImage(m_Image);

			m_ColRadius = m_Image.width * 0.5f;

		}

		public void SetParent(GameObject _parent)
		{
			//remove from previous parent
			if (m_Parent != null)
			{
				m_Parent.m_Children.Remove(this);
			}

			//set new parent
			m_Parent = _parent;

			//add to new parent
			if(m_Parent != null)
			{
				_parent.m_Children.Add(this);
			}
		}

		//We want the derived class to override this function
		public virtual void Update(float _deltatime)
		{
			
		}

		public void UpdateTransforms()
		{
			//the parent object's global position multiplied by the offset of the child object gives us the childs global position
			//if there's no parent, this object's local position is it's global position
			if (m_Parent != null)
			{
				m_GlobalTransfrom = m_Parent.m_GlobalTransfrom * m_LocalTransform;
			}
			else
			{
				m_GlobalTransfrom = m_LocalTransform;
			}

			//loop through all child objects and update their transforms
			foreach(GameObject child in m_Children)
			{
				child.UpdateTransforms();
			}
			//after computing position for the frame, store where we are
			//m_PreviousPos = GetPosition() - m_Parent.GetPosition();
		}

		public virtual void Draw()
		{
			Renderer.DrawTexture(m_Texture, m_GlobalTransfrom, RLColor.WHITE.ToColor());

		}

		
		public virtual void OnCollision(GameObject _otherObj)
		{
			
		}

		public Vector2 GetGlobalPosition()
		{
			return new Vector2(m_GlobalTransfrom.m7, m_GlobalTransfrom.m8);
		}
		


	}
}
