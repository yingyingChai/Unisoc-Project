using System;
namespace KaYi.Web.Infrastructure.Model.Evaluator
{
	public class EvaluateLevel
	{
		private float score;
		private int count;
		private float percent;
		private float myScore;
		public float Score
		{
			get
			{
				return this.score;
			}
			set
			{
				this.score = value;
			}
		}
		public int Count
		{
			get
			{
				return this.count;
			}
			set
			{
				this.count = value;
			}
		}
		public float Percent
		{
			get
			{
				return this.percent;
			}
			set
			{
				this.percent = value;
			}
		}
		public float MyScore
		{
			get
			{
				return this.myScore;
			}
			set
			{
				this.myScore = value;
			}
		}
	}
}
