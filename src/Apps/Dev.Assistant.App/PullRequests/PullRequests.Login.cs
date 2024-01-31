using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Utilities;

namespace Dev.Assistant.App.PrReview;

public partial class PrReviewLogin : UserControl
{
    private readonly AppHome _appHome;

    public PrReviewLogin(AppHome appHome)
    {
        InitializeComponent();

        _appHome = appHome;
        ActiveControl = PatInput;
    }

    private void GoBtn_Click(object sender, EventArgs e)
    {
        Event ev = DevEvents.MWReviewedByMe;

        _appHome.LogEvent(ev, EventStatus.Clicked);

        try
        {
            if (string.IsNullOrWhiteSpace(PatInput.Text))
            {
                string errMesg = "Please enter your username";

                _appHome.LogError(errMesg, ev);

                return;
            }

            //Consts.UserSettings.Pat = PatInput.Text;

            _appHome.LogEvent(ev, EventStatus.Succeed);
        }
        catch (DevAssistantException ex)
        {
            _appHome.LogError(ex);
        }
        catch (Exception ex)
        {
            _appHome.LogError(new DevAssistantException(ex.Message, 7403));
            _appHome.LogEvent(ev, EventStatus.Failed, ex: ex);
        }
    }
}