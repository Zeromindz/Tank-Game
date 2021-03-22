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

		// m1 - m4 - m7
		// m2 - m5 - m8
		// m3 - m6 - m9

		public GameObject(string _fileName)
		{
			//load image and convert to texture
			m_Image = LoadImage(_fileName);
			m_Texture = LoadTextureFromImage(m_Image);

			//starting position
			m_LocalTransform.m1 = 1;
			m_LocalTransform.m2 = 0;
			m_LocalTransform.m3 = 0;
			m_LocalTransform.m4 = 0;
			m_LocalTransform.m5 = 1;
			m_LocalTransform.m6 = 0;
			m_LocalTransform.m7 = 100;
			m_LocalTransform.m8 = 200;
			m_LocalTransform.m9 = 1;
			
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
		}

		public void Draw()
		{
			Renderer.DrawTexture(m_Texture, m_GlobalTransfrom, RLColor.WHITE.ToColor());

		}
	}
}
