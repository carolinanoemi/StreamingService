USE StreamingService_Ratings;
GO

-- Gennemsnitlig rating pr. video
SELECT
  VideoId,
  AVG(CAST(Score AS float)) AS AvgScore,
  COUNT(*) AS RatingCount
FROM dbo.Ratings
GROUP BY VideoId;

-- Top 10 mest rated videoer
SELECT TOP 10
  VideoId,
  COUNT(*) AS RatingCount
FROM dbo.Ratings
GROUP BY VideoId
ORDER BY RatingCount DESC;



