using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisAcademyApp.Data.Models;

namespace TennisAcademyApp.Data.Configurations
{
    public class SurfaceConfiguration : IEntityTypeConfiguration<Surface>
    {
        public void Configure(EntityTypeBuilder<Surface> config)
        {
            config
                .HasData(SeededSurfaces());
        }
        private List<Surface> SeededSurfaces()
        {
            List<Surface> seededSurfaces = new List<Surface>()
            {
                new Surface
                {
                    Id = 1,
                    Name = "Clay",
                    ImageUrl = "https://www.google.com/imgres?q=clay%20court&imgurl=https%3A%2F%2Fcdn11.bigcommerce.com%2Fs-pop81y%2Fimages%2Fstencil%2F960x545%2Fuploaded_images%2Fallstartennissupply-281535-clay-tennis-courts-blogbanner1.jpg%3Ft%3D1703002083&imgrefurl=https%3A%2F%2Fwww.allstartennissupply.com%2Fblog%2Fwhat-is-the-best-climate-for-clay-tennis-courts%2F%3Fsrsltid%3DAfmBOorm03gyRg52IMAFa7-l2ig3k_9l9SE1UjjQCmsplj7SJUMqY2Ci&docid=wGtAwbkIqo2SLM&tbnid=587J-uncERjX8M&vet=12ahUKEwjRzYidybaOAxVeX_EDHZpnGxwQM3oECFEQAA..i&w=960&h=539&hcb=2&ved=2ahUKEwjRzYidybaOAxVeX_EDHZpnGxwQM3oECFEQAA"
                },
                new Surface
                {
                    Id = 2,
                    Name = "Hard",
                    ImageUrl = "https://www.google.com/imgres?q=Hard%20court&imgurl=https%3A%2F%2Fwww.edwardssports.co.uk%2Fpub%2Fmedia%2Fwysiwyg%2FPlaying_On_A_Hard_Tennis_Court.jpg&imgrefurl=https%3A%2F%2Fwww.edwardssports.co.uk%2Fnews%2Fpost%2Fclay-court-vs-hard-court-tennis&docid=_e_VzZEOyVdxeM&tbnid=O670DpSc8HOqTM&vet=12ahUKEwir89etybaOAxW6evEDHSxsBQ0QM3oECB0QAA..i&w=900&h=500&hcb=2&ved=2ahUKEwir89etybaOAxW6evEDHSxsBQ0QM3oECB0QAA"
                },
                new Surface
                {
                    Id = 3,
                    Name = "Grass",
                    ImageUrl = "https://www.google.com/imgres?q=Grass%20court&imgurl=https%3A%2F%2Fi.abcnewsfe.com%2Fa%2F172020d0-16bb-4c84-a3d2-b436b77d5f7e%2Fwimbledon5-2023-gty-ml-240614_1718369987464_hpMain.jpg&imgrefurl=https%3A%2F%2Fabcnews.go.com%2FUS%2Fstaggering-science-art-wimbledons-legendary-grass-courts%2Fstory%3Fid%3D111433116&docid=7Ne81Wn1LUAVtM&tbnid=f5ihWuIF1wvqzM&vet=12ahUKEwjyxZTSybaOAxVaSfEDHU1jGa4QM3oECBoQAA..i&w=3072&h=2048&hcb=2&ved=2ahUKEwjyxZTSybaOAxVaSfEDHU1jGa4QM3oECBoQAA"
                },
            };
            return seededSurfaces;
        }
    }
}
