using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using TownSquareAPI.Models;


namespace TownSquareAPI.Services;

public class ProfilePictureService
{
    private readonly IMongoCollection<ProfilePicture> _profilePictures;

    public ProfilePictureService(IMongoClient mongoClient)
    {
        // Connect to MongoDB database "ProfilePictures"
        var database = mongoClient.GetDatabase("ProfilePictures");

        // Access the collection named "Pictures"
        _profilePictures = database.GetCollection<ProfilePicture>("Pictures");
    }

    // Get ProfilePicture by UserId
    public async Task<ProfilePicture> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _profilePictures
            .Find(pic => pic.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    // Create a new ProfilePicture
    public async Task<ProfilePicture> CreateOrReplaceAsync(ProfilePicture profilePicture, CancellationToken cancellationToken)
    {
        var existing = await _profilePictures.Find(p => p.UserId == profilePicture.UserId).FirstOrDefaultAsync(cancellationToken);

        if (existing != null)
        {
            profilePicture.Id = existing.Id;
        }
        else
        {
            profilePicture.Id = ObjectId.GenerateNewId(); // or leave it if already generated
        }

        var filter = Builders<ProfilePicture>.Filter.Eq(p => p.UserId, profilePicture.UserId);
        var options = new ReplaceOptions { IsUpsert = true };

        await _profilePictures.ReplaceOneAsync(filter, profilePicture, options, cancellationToken);

        return profilePicture;
    }

    // Delete ProfilePicture by userId
    public async Task<bool> DeleteAsync(string userId, CancellationToken cancellationToken)
    {
        var result = await _profilePictures.DeleteOneAsync(p => p.UserId == userId, cancellationToken);
        return result.DeletedCount > 0;
    }
}
