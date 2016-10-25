using System;
using System.Collections.Generic;

namespace MyStockViewApp
{
    public class PropertyAction
    {
        public string PropertyName;
        public string value;
    };

    public class ViewModelMediator : NotifyPropertyChanged
    {
        static ViewModelMediator instance = null;
        public IDataManager dataManager = new DataManager();
        public ILogManager logManager = Utilities.LogManager; //new LogManager();
        public IStatusInfoManager statusManager = Utilities.StatusManager;//new StatusInfoManager();
        Dictionary<string, List<object>> actionSubscribers = new Dictionary<string, List<object> >();
        Dictionary<string, List<object>> actionPublishers = new Dictionary<string, List<object> >();


        private PropertyAction action;
        public PropertyAction Action
        {
            get { return action; }
            set
            {
                action = value;
                OnPropertyChanged("Action");
            }
        }


        public ViewModelMediator()
        {
            this.PropertyChanged += (obj, args) =>
            {
                if (args.PropertyName == "Action")
                {
                    actionSubscribers[Action.PropertyName].ForEach(o => (o as IExchangeSelection).SelectedExchange = Action.value);
                }
            };
        }
        public static ViewModelMediator GetMediator()
        {
            if (instance == null)
                instance = new ViewModelMediator();
            return instance;
        }

        public void RegisterForAction(string action, bool publish,  object obj)
        {
            if (publish)
            {
                if (!actionPublishers.ContainsKey(action))
                    actionPublishers[action] = new List<object>();
                actionPublishers[action].Add(obj);
            }
            else
            {
                if (!actionSubscribers.ContainsKey(action))
                    actionSubscribers[action] = new List<object>();
                actionSubscribers[action].Add(obj);
            }
        }
    }
}
