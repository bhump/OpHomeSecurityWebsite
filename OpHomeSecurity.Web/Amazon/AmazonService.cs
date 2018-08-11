using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace OpHomeSecurity.Web.Amazon
{
    public class AmazonService
    {
        private string profileName = ConfigurationManager.AppSettings["AWSProfileName"];
        private string bucket = ConfigurationManager.AppSettings["AWSBucket"];
        private string accessId = ConfigurationManager.AppSettings["AWSAccessKey"];
        private string secretId = ConfigurationManager.AppSettings["AWSSecretKey"];
        private string region = ConfigurationManager.AppSettings["AWSRegion"];

        private IAmazonS3 _client;

        public AmazonService()
        {
            _client = new AmazonS3Client(accessId, secretId);
        }

        public string PutObjectToAmazon(string fileId, Stream fileContents)
        {
            var request = new PutObjectRequest();
            request.BucketName = bucket;
            request.Key = fileId;
            request.CannedACL = S3CannedACL.PublicRead;
            request.InputStream = fileContents;
            request.StorageClass = S3StorageClass.ReducedRedundancy;

            var response = _client.PutObject(request);

            return "Uploaded";
        }

        public string DeleteObjectFromAmazon(string fileId)
        {
            var request = new DeleteObjectRequest();
            request.BucketName = bucket;
            request.Key = fileId;

            var response = _client.DeleteObject(request);

            return "Removed";
        }
    }
}