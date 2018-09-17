using KaYi.Database;
using KaYi.FileSystem.Model;
using System;
using System.Collections.Generic;
namespace KaYi.FileSystem.DAL
{
	public class FileHitGateway
	{
		private DALGateway<FileHit> dbGateway = new DALGateway<FileHit>();
		public FileHitGateway()
		{
			this.dbGateway.LoadSchema("FILE_HITS");
		}
		public void AddNew(FileHit newFileHit)
		{
			this.dbGateway.AddNew(newFileHit);
		}
		public void DeleteByFileHitID(string FileHitID)
		{
			this.dbGateway.DeleteByFieldValue("HitID", FileHitID);
		}
		public void UpdateByPK(FileHit objFileHit)
		{
			this.dbGateway.UpdateByFieldValue("HitID", objFileHit.HitID, objFileHit);
		}
		public IList<FileHit> GetFileHitsBy(string FileID)
		{
			return null;
		}
		public FileHit GetFileHitByID(string FileHitID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("HitID", Operator.EqualTo, FileHitID));
			return this.dbGateway.getRecord(conditions);
		}
	}
}
