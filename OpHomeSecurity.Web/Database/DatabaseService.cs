using OpHomeSecurity.Web.Amazon;
using OpHomeSecurity.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace OpHomeSecurity.Web.Database
{
    public class DatabaseService
    {
        private dboOpHomeSecurityEntities _context;

        public DatabaseService()
        {
            _context = new dboOpHomeSecurityEntities();
        }

        public bool CreateAlbum(string albumName, bool isFeatured)
        {
            try
            {
                using (_context)
                {
                    tAlbum album = new tAlbum();
                    album.Active = true;
                    album.AlbumId = Guid.NewGuid();
                    album.Name = albumName;
                    album.IsFeatured = isFeatured;

                    _context.tAlbums.Add(album);
                    _context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<AlbumModel> GetAlbums()
        {
            return _context.tAlbums.Select(a => new AlbumModel { AlbumName = a.Name, AlbumId = a.AlbumId, IsActive = a.Active, IsFeatured = a.IsFeatured }).ToList();
        }

        public bool CreateImage(List<ImageModel> images)
        {
            AmazonService amzService = new AmazonService();

            bool isSuccess = false;

            try
            {
                using (_context)
                {
                    foreach (var image in images)
                    {

                        byte[] imageBytes = null;

                        tImage newImage = new tImage();
                        newImage.Active = true;
                        newImage.ImageId = Guid.NewGuid();
                        newImage.Name = image.ImageName;
                        newImage.AlbumId = image.AlbumId;
                        newImage.IsFeatured = image.IsFeatured;

                        _context.tImages.Add(newImage);

                        using (var binaryReader = new BinaryReader(image.InputStream))
                        {
                            imageBytes = binaryReader.ReadBytes(image.ContentLength);
                            MemoryStream memoryStream = new MemoryStream(imageBytes);
                            amzService.PutObjectToAmazon("Gallery/" + newImage.ImageId.ToString(), memoryStream);
                        }
                    }

                    _context.SaveChanges();
                }

                return isSuccess = true;
            }
            catch (Exception ex)
            {
                return isSuccess;
            }
        }

        public bool CreateImage(ImageModel image, Guid productId)
        {
            AmazonService amzService = new AmazonService();

            bool isSuccess = false;

            try
            {
                using (_context)
                {
                    byte[] imageBytes = null;

                    var toUpdate = _context.tProducts.Where(p => p.ProductId == productId).FirstOrDefault();
                    toUpdate.ImageUrl = image.ImageName;

                    using (var binaryReader = new BinaryReader(image.InputStream))
                    {
                        imageBytes = binaryReader.ReadBytes(image.ContentLength);
                        MemoryStream memoryStream = new MemoryStream(imageBytes);
                        amzService.PutObjectToAmazon("Products/" + toUpdate.ProductId.ToString(), memoryStream);
                    }

                    _context.SaveChanges();
                }

                return isSuccess = true;
            }
            catch (Exception ex)
            {
                return isSuccess;
            }
        }

        public List<ImageModel> GetImages(string albumId)
        {
            return _context.tImages.Where(i => i.AlbumId == new Guid(albumId)).Select(i => new ImageModel { ImageName = i.Name, IsActive = i.Active, ImageId = i.ImageId, AlbumId = i.AlbumId, IsFeatured = i.IsFeatured }).ToList();
        }

        public bool DeleteImage(Guid imageId)
        {
            using (_context)
            {
                try
                {
                    AmazonService amzService = new AmazonService();

                    amzService.DeleteObjectFromAmazon("Gallery/" + imageId.ToString());

                    var imageToDelete = _context.tImages.Where(i => i.ImageId == imageId).First();

                    _context.tImages.Remove(imageToDelete);

                    _context.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool DeleteAlbum(Guid albumId)
        {

            bool isSuccess = true;

            try
            {
                var getImages = GetImages(albumId.ToString());


                using (_context)
                {
                    foreach (var image in getImages)
                    {
                        AmazonService amzService = new AmazonService();

                        amzService.DeleteObjectFromAmazon("Gallery/" + image.ImageId.ToString());

                        var imageToDelete = _context.tImages.Where(i => i.ImageId == image.ImageId).First();

                        _context.tImages.Remove(imageToDelete);
                    }

                    var albumToDelete = _context.tAlbums.Where(a => a.AlbumId == albumId).First();

                    _context.tAlbums.Remove(albumToDelete);

                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public List<tAlbum> GetImages()
        {
            var albums = _context.tAlbums.Select(a => a).ToList();

            return albums;
        }

        public bool CreateTestimonial(TestimonialModel testimonial)
        {
            var isSuccess = true;

            try
            {
                using (_context)
                {
                    tTestimonial test = new tTestimonial();
                    test.Active = true;
                    test.ByName = testimonial.ByName;
                    test.Location = testimonial.Location;
                    test.Testimonial = testimonial.Testimonial;
                    test.TestimonialId = Guid.NewGuid();

                    _context.tTestimonials.Add(test);
                    _context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool DeleteTestimonial(Guid testimonialId)
        {
            bool isSuccess = true;

            try
            {
                var toDelete = _context.tTestimonials.Where(t => t.TestimonialId == testimonialId).First();

                _context.tTestimonials.Remove(toDelete);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public bool UpdateTestimonial(TestimonialModel model)
        {
            bool isSuccess = true;

            try
            {
                var toUpdate = _context.tTestimonials.Where(t => t.TestimonialId == model.TestimonialId).First();
                toUpdate.Active = model.Active;
                toUpdate.ByName = model.ByName;
                toUpdate.Location = model.Location;
                toUpdate.Testimonial = model.Testimonial;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public List<TestimonialModel> GetTestimonials()
        {
            return _context.tTestimonials.Select(t => new TestimonialModel { Active = t.Active, ByName = t.ByName, Location = t.Location, Testimonial = t.Testimonial, TestimonialId = t.TestimonialId }).ToList();
        }

        public List<ProductModel> GetProducts()
        {
            return _context.tProducts.Select(p => new ProductModel { Description = p.Description, ImageUrl = p.ImageUrl, Name = p.Name, ProductId = p.ProductId }).ToList();
        }

        public bool CreateProduct(ProductModel model, ImageModel image)
        {
            bool isSuccess = false;

            try
            {
                var product = new tProduct();
                product.ProductId = Guid.NewGuid();
                product.Name = model.Name;
                product.Description = model.Description;
                product.ImageUrl = "Empty";

                _context.tProducts.Add(product);
                _context.SaveChanges();

                if (image != null)
                {
                    isSuccess = CreateImage(image, product.ProductId);
                }

                isSuccess = true;

            }
            catch (Exception ex)
            {
                isSuccess = false;
                Debug.WriteLine(ex.ToString());
            }

            return isSuccess;
        }

        public bool DeleteProduct(Guid productId)
        {
            bool isSuccess = false;

            try
            {
                AmazonService amzService = new AmazonService();

                amzService.DeleteObjectFromAmazon("Products/" + productId.ToString());

                var toDelete = _context.tProducts.Where(p => p.ProductId == productId).FirstOrDefault();

                _context.tProducts.Remove(toDelete);
                _context.SaveChanges();

                isSuccess = true;
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}