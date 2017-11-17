namespace ImageEmotions.Models
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Web;
    using Newtonsoft.Json.Linq;
    using System;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class EmotionsViewModel
    {
        public EmotionsViewModel(string Json)
        {
            EmotionsModel model = new EmotionsModel();

            try
            {

                RootObject[] obj = JsonConvert.DeserializeObject<RootObject[]>(Json);

                //foreach (var emotion in obj)
                var emotion = obj.FirstOrDefault();

                model.Scores = new List<EmotionScore>()
                    {
                        new EmotionScore("anger", emotion.scores.anger*100),
                        new EmotionScore("contempt", emotion.scores.contempt*100),
                        new EmotionScore("disgust", emotion.scores.disgust*100),
                        new EmotionScore("fear", emotion.scores.fear*100),
                        new EmotionScore("happiness", emotion.scores.happiness*100),
                        new EmotionScore("neutral", emotion.scores.neutral*100),
                        new EmotionScore("sadness", emotion.scores.sadness*100),
                        new EmotionScore("surprise", emotion.scores.surprise*100)
                    };
                model.Face = new Rectangle(emotion.faceRectangle.left, emotion.faceRectangle.top,
                    emotion.faceRectangle.width, emotion.faceRectangle.height);

            }
            catch (Exception ex)
            {
                //System.Diagnostics.Debugger.Break();
                Console.WriteLine(ex.Message);
            }

            this.Emotions = model;
        }

        public EmotionsModel Emotions { get; set; }

        public Image Image { get; set; }
    }

    public class EmotionsModel
    {
        public EmotionsModel()
        {
            Face = new Rectangle();
            Scores = new List<EmotionScore>();
        }

        public Rectangle Face { get; set; }
        public List<EmotionScore> Scores { get; set; }
    }

    public class EmotionScore
    {
        public EmotionScore(string Key, double Value)
        {
            this.key = Key;
            this.value = Value;
        }

        public string key { get; set; }
        public double value { get; set; }
    }



    //Generated
    public class FaceRectangle
    {
        public int height { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
    }

    public class Scores
    {
        public double anger { get; set; }
        public double contempt { get; set; }
        public double disgust { get; set; }
        public double fear { get; set; }
        public double happiness { get; set; }
        public double neutral { get; set; }
        public double sadness { get; set; }
        public double surprise { get; set; }
    }

    public class RootObject
    {
        public FaceRectangle faceRectangle { get; set; }
        public Scores scores { get; set; }
    }
}