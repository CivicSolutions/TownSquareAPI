using Microsoft.EntityFrameworkCore;
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
    public async Task<ProfilePicture> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await _profilePictures
            .Find(pic => pic.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    // Create a new ProfilePicture
    public async Task<ProfilePicture> CreateAsync(ProfilePicture profilePicture, CancellationToken cancellationToken)
    {
        await _profilePictures.InsertOneAsync(profilePicture, null, cancellationToken);
        return profilePicture;
    }

    // Update existing ProfilePicture by userId
    public async Task<ProfilePicture> UpdateAsync(int userId, ProfilePicture updatedProfilePicture, CancellationToken cancellationToken)
    {
        var filter = Builders<ProfilePicture>.Filter.Eq(p => p.UserId, userId);
        var result = await _profilePictures.ReplaceOneAsync(filter, updatedProfilePicture, cancellationToken: cancellationToken);

        if (result.MatchedCount == 0)
            return null; // no document found with this userId

        return updatedProfilePicture;
    }

    // Delete ProfilePicture by userId
    public async Task<bool> DeleteAsync(int userId, CancellationToken cancellationToken)
    {
        var result = await _profilePictures.DeleteOneAsync(p => p.UserId == userId, cancellationToken);
        return result.DeletedCount > 0;
    }
}
