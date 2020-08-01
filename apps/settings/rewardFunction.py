reward = 0.0

if passedCheckpoint:
    reward += 1.0

if isCarReversed:
    reward -= 1.0
else:
    reward += (distanceToCheckpoint * 0.03)
    reward += (speed * 0.02)

if speed > 0.0:
    reward += 0.05

count = 0
for index, value in ipairs(distanceToWall):
    if (count == 0) or (count == 2) or (count == 3) or (count == 4) or (count == 6):
        if value < 0.25:
            reward -= 1.0
    if (count == 1) or (count == 5):
        if value < 0.1:
            reward -= 1.0
    count += 1
    
if isOutBound:
    reward -= 1.0

return reward
