using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImageEmotions.Models
{

    public class EmotionsViewModel
    {
        public EmotionsViewModel(string Json)
        {
            EmotionsModel model = new EmotionsModel();

            try
            {

                EmotionsMatrix[] obj = JsonConvert.DeserializeObject<EmotionsMatrix[]>(Json);

                //TODO: support multiple responses
                //foreach (var emotion in obj)
                var emotion = obj.FirstOrDefault();

                model.Scores = new List<EmotionScore>()
                    {
                        new EmotionScore("anger", emotion.FaceAttributes.Emotion.Anger*100),
                        new EmotionScore("contempt", emotion.FaceAttributes.Emotion.Contempt*100),
                        new EmotionScore("happiness", emotion.FaceAttributes.Emotion.Happiness*100),
                        new EmotionScore("surprise", emotion.FaceAttributes.Emotion.Surprise*100),
                        new EmotionScore("disgust", emotion.FaceAttributes.Emotion.Disgust*100),
                        new EmotionScore("fear", emotion.FaceAttributes.Emotion.Fear*100),
                        new EmotionScore("sadness", emotion.FaceAttributes.Emotion.Sadness*100),
                        new EmotionScore("neutral", emotion.FaceAttributes.Emotion.Neutral*100)
                    };
                model.Face = new Rectangle(emotion.FaceRectangle.Left, emotion.FaceRectangle.Top,
                    emotion.FaceRectangle.Width, emotion.FaceRectangle.Height);

            }
            catch (Exception ex)
            {
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
        public EmotionScore(string key, double value)
        {
            this.Key = key;
            this.Value = value;
        }

        public string Key { get; set; }
        public double Value { get; set; }
        public string Color { get; set; }
    }
}