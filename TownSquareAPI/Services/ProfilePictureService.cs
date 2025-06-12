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

    // Update existing ProfilePicture by Id
    public async Task<ProfilePicture> UpdateAsync(int id, ProfilePicture updatedProfilePicture, CancellationToken cancellationToken)
    {
        var filter = Builders<ProfilePicture>.Filter.Eq(p => p.Id, id);
        var result = await _profilePictures.ReplaceOneAsync(filter, updatedProfilePicture, cancellationToken: cancellationToken);

        if (result.MatchedCount == 0)
            return null; // no document found with this id

        return updatedProfilePicture;
    }

    // Delete ProfilePicture by Id
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _profilePictures.DeleteOneAsync(p => p.Id == id, cancellationToken);
        return result.DeletedCount > 0;
    }
}
