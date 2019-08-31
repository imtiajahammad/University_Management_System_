using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem06.DataAccessLogics.Designation;
using UniversityManagementSystem06.Models;

namespace UniversityManagementSystem06.BusinessLogics.Designation
{
    public class DesignationManager
    {
        public List<DesignationModel> GetAllDesignations()
        {
            DesignationGateway aDesignationGateway = new DesignationGateway();
            List<DesignationModel> designations = new List<DesignationModel>();
            designations = aDesignationGateway.GetAllDesignations();
            return designations;
        }
        public DesignationModel GetDesignationById(int designationId)
        {
            DesignationGateway aDesignationGateway = new DesignationGateway();
            DesignationModel aDesignationModel = new DesignationModel();
            aDesignationModel = aDesignationGateway.GetDesignationById(designationId);
            return aDesignationModel;
        }
    }
}