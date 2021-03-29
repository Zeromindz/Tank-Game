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
		protected bool m_EnabledCollision = true;
		protected Vector2 m_Min = new Vector2(0, 0);
		protected Vector2 m_Max = new Vector2(0, 0);
		protected float m_ColRadius = 0.0f;
		public Vector2 m_PreviousPos;

		bool m_Alive;
		// m1 - m4 - m7
		// m2 - m5 - m8
		// m3 - m6 - m9

		public GameObject(string _fileName)
		{
			//load image and convert to texture
			m_Image = LoadImage(_fileName);
			m_Texture = LoadTextureFromImage(m_Image);

			m_Min.x = (float)-(m_Texture.width * 0.5);
			m_Min.y = (float)-(m_Texture.width * 0.5);

			m_Max.x = (float)(m_Texture.width * 0.5);
			m_Max.y = (float)(m_Texture.width * 0.5);

			m_ColRadius = m_Image.height * 0.5f;

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

			if (m_Parent != null)
				m_PreviousPos = GetGlobalPosition() - m_Parent.GetGlobalPosition();
			else
				m_PreviousPos = GetGlobalPosition();
				
			//after computing position for the frame, store where we are
			//m_PreviousPos = GetPosition() - m_Parent.GetPosition();
		}

		public virtual void Draw()
		{
			if(!m_Alive)
			{
				return;
			}

			Renderer.DrawTexture(m_Texture, m_GlobalTransfrom, RLColor.WHITE.ToColor());

		}

		
		public virtual void OnCollision(GameObject _otherObj)
		{
			
		}

		public Vector2 GetGlobalPosition()
		{
			return new Vector2(m_GlobalTransfrom.m7, m_GlobalTransfrom.m8);
		}
		
		public bool GetCollisionEnabled()
		{
			return m_EnabledCollision;
		}

		public Vector2 GetMin()
		{
			return m_Min;
		}

		public Vector2 GetMax()
		{
			return m_Max;
		}

		public bool GetAlive()
		{
			return m_Alive;
		}

		public void SetAlive(bool _alive)
		{
			m_Alive = _alive;
		}
	}
}
