using System;
using System.Drawing;
using System.Windows.Forms;
using Demcon.ProductionTool.General;
using Demcon.ProductionTool.Model;
using Demcon.ProductionTool.View.FatTestPages;

namespace Demcon.ProductionTool.View
{
    public partial class TestStepListItem : UserControl
    {
        public TestStepListItem(IConclusionItem conclusionItem, ImageList images)
        {
            InitializeComponent();
            this.ConclusionItem = conclusionItem;
            //this.ConclusionItem.UpdatedEvent += new EventHandler<EventArgs<bool>>(HandleTestUpdatedEvent);

            this.Images = images;
            this.isHighlighted = false;
            this.UpdateComponents();
        }

        private void ClickForward(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        public IConclusionItem ConclusionItem { get; private set; }

        private bool isHighlighted;
        public bool IsHighlighted
        {
            get
            {
                return this.isHighlighted;
            }

            set
            {
                this.isHighlighted = value;
                this.UpdateComponents();
            }
        }

        private ImageList Images { get; set; }

        private void HandleTestUpdatedEvent(object sender, EventArgs<bool> e)
        {
            this.BeginInvoke((Action)(() => { this.UpdateComponents(); }));
        }

        private void UpdateComponents()
        {
            if (this.IsHighlighted)
            {
                this.BackColor = Color.LightGray;
                this.ResultPicture.Image = Images.Images[(int)TestPage.ImageIndexes.Arrow];
            }
            else
            {
                this.BackColor = SystemColors.Control;
                switch (this.ConclusionItem.Conclusion)
                {
                    case Model.ETestConclusion.NotTested:
                        this.ResultPicture.Image = Images.Images[(int)TestPage.ImageIndexes.Nothing];
                        break;
                    case Model.ETestConclusion.Passed:
                        this.ResultPicture.Image = Images.Images[(int)TestPage.ImageIndexes.Check];
                        break;
                    case Model.ETestConclusion.Failed:
                        this.ResultPicture.Image = Images.Images[(int)TestPage.ImageIndexes.Cross];
                        break;
                    default:
                        this.ResultPicture.Image = Images.Images[(int)TestPage.ImageIndexes.Inconclusive];
                        break;
                }
                this.ResultPicture.SizeMode = PictureBoxSizeMode.CenterImage;
            }

            this.NameLabel.Text = this.ConclusionItem.Name;
         }
    }
}
