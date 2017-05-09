using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes.Utilities
{
    public class GenericAccessRole
    {
        private bool _CreatePremission = false;
        private bool _DeletePremission = false;
        private bool _EditPremission = false;
        private bool _DetailsPremission = false;

        public bool CreatePremission
        {
            get
            {
                return _CreatePremission;
            }

            set
            {
                _CreatePremission = value;
            }
        }
        public bool DeletePremission
        {
            get
            {
                return _DeletePremission;
            }

            set
            {
                _DeletePremission = value;
            }
        }
        public bool EditPremission
        {
            get
            {
                return _EditPremission;
            }

            set
            {
                _EditPremission = value;
            }
        }
        public bool DetailsPremission
        {
            get
            {
                return _DetailsPremission;
            }

            set
            {
                _DetailsPremission = value;
            }
        }
    }

    public class DeliveryRequestAccessRole : GenericAccessRole
    {
        private bool _RececivedPremission = false;
        private bool _AssignPremission = false;
        private bool _Deliveryremission = false;

        public bool RececivedPremission
        {
            get
            {
                return _RececivedPremission;
            }

            set
            {
                _RececivedPremission = value;
            }
        }
        public bool AssignPremission
        {
            get
            {
                return _AssignPremission;
            }

            set
            {
                _AssignPremission = value;
            }
        }
        public bool DeliveryPremission
        {
            get
            {
                return _Deliveryremission;
            }

            set
            {
                _Deliveryremission = value;
            }
        }
    }
}