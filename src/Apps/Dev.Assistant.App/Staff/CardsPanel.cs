using Dev.Assistant.Business.Core.Models;
using System.Collections.ObjectModel;

namespace Dev.Assistant.App.Staff;

public class CardsPanel : Panel
{
    private const int CardWidth = 200;
    private const int CardHeight = 150;

    public event EventHandler OnClicked;

    public CardsViewModel ViewModel { get; set; }

    public CardsPanel()
    {
    }

    public CardsPanel(CardsViewModel viewModel)
    {
        ViewModel = viewModel;
        ViewModel.Cards.CollectionChanged += Cards_CollectionChanged;
    }

    private void Cards_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        DataBind();
    }

    public void DataBind()
    {
        SuspendLayout();
        Controls.Clear();

        for (int i = 0; i < ViewModel.Cards.Count; i++)
        {
            var newCtl = new CardControl(ViewModel.Cards[i]);

            //newCtl.OnClicked += OnClicked;

            newCtl.TabIndex = i;

            newCtl.DataBind();
            SetCardControlLayout(newCtl, i);
            Controls.Add(newCtl);
        }

        ResumeLayout();
    }

    private void SetCardControlLayout(CardControl ctl, int atIndex)
    {
        ctl.Width = CardWidth;
        ctl.Height = CardHeight;

        //calc visible column count
        int columnCount = Width / ctl.Width;

        if (columnCount == 0)
            columnCount = 2;

        //calc the x index and y index.
        int xPos = (atIndex % columnCount) * ctl.Width;
        int yPos = (atIndex / columnCount) * ctl.Height;

        ctl.Location = new Point(xPos, yPos);
    }
}

public class CardsViewModel
{
    public ObservableCollection<EmployeeInfo> Cards { get; set; }
}