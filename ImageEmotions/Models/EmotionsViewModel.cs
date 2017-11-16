namespace ImageEmotions.Models
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Web;
    using Newtonsoft.Json.Linq;
    using System;

    public class EmotionsViewModel
    {
        public EmotionsViewModel(string Json)
        {
            //TODO: stuff
            //Parse Json into Emotions

            JObject joResponse = JObject.Parse(Json);
            JObject ojObject = (JObject)joResponse["response"];
            JArray array = (JArray)ojObject["chats"];
            int id = Convert.ToInt32(array[0].ToString());

            Emotions = new List<EmotionsModel>();
            EmotionsModel model = new EmotionsModel();

            model.Scores = new List<EmotionScore>();
            EmotionScore emo = new EmotionScore();
            emo.key = "anger";
            emo.value = 70;

            model.Scores.Add(emo);
            model.Face = new ImageFaceModel();

            Emotions.Add(model);
        }

        public ICollection<EmotionsModel> Emotions { get; set; }

        public Image Image { get; set; }
    }

    public class EmotionsModel
    {
        public EmotionsModel()
        {
            Face = new ImageFaceModel();
            Scores = new List<EmotionScore>();
        }

        public ImageFaceModel Face { get; set; }
        public List<EmotionScore> Scores { get; set; }
    }

    public class ImageFaceModel
    {
        public ImageFaceModel()
        {
            Face = new Rectangle();
            Image = @"http://4vector.com/i/free-vector-rubik-s-cube-random-clip-art_106251_Rubiks_Cube_Random_clip_art_medium.png";
        }

        public Rectangle Face { get; set; }

        public string Image { get; set; }

    }

    public class EmotionScore
    {
        public string key { get; set; }
        public float value { get; set; }
    }
}